using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using System.Windows.Input;
using System.ComponentModel;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.Specialized;

namespace Team1922.MVVM.ViewModels
{
    public class RobotViewModelBase : ViewModelBase<Robot>, IRobotProvider, IDisposable
    {
        public RobotViewModelBase() : base(null)
        {
            _subsystemProviders = new CompoundProviderList<ISubsystemProvider, Subsystem>("Subsystems", this, (Subsystem model) => { return new SubsystemViewModel(this) { ModelReference = model }; });
            _eventHandlerProviders = new CompoundProviderList<IEventHandlerProvider, Models.EventHandler>("EventHandlers", this, (Models.EventHandler model) => { return new EventHandlerViewModel(this) { ModelReference = model }; });
            _joystickProviders = new CompoundProviderList<IJoystickProvider, Joystick>("Joysticks", this, (Joystick model) => { return new JoystickViewModel(this) { ModelReference = model }; });

            _hierarchialAccesCTS = new CancellationTokenSource();

            //start the background thread which processes the access queue
            _hierarchialAccessTask = Task.Run((Action)WorkerThreadMethod);
            //then start the background thread which removes the dead tickets after the access queue processes them
            _hierarchailAccessDeadTokenCleanupTask = Task.Run((Action)DeadTicketCleanup);
        }

        #region IRobotProvider
        public IObservableCollection<ISubsystemProvider> Subsystems
        {
            get { return _subsystemProviders.Items; }
        }
        public IObservableCollection<IEventHandlerProvider> EventHandlers
        {
            get { return _eventHandlerProviders.Items; }
        }
        public IObservableCollection<IJoystickProvider> Joysticks
        {
            get { return _joystickProviders.Items; }
        }
        public IRobotMapProvider RobotMap
        {
            get
            {
                return _robotMapProvider;
            }
        }
        public void AddSubsystem(Subsystem subsystem)
        {
            AddSubsystem(subsystem, true);
        }
        public void AddJoystick(Joystick joystick)
        {
            AddJoystick(joystick, true);
        }
        public void AddEventHandler(Models.EventHandler eventHandler)
        {
            AddEventHandler(eventHandler, true);
        }
        public int TeamNumber
        {
            get
            {
                return ModelReference.TeamNumber;
            }

            set
            {
                ModelReference.TeamNumber = value;
            }
        }
        public int AnalogInputSampleRate
        {
            get
            {
                return ModelReference.AnalogInputSampleRate;
            }

            set
            {
                ModelReference.AnalogInputSampleRate = value;
            }
        }
        public void RemoveSubsystem(string name)
        {
            _subsystemProviders.Remove(name);
        }
        public void RemoveJoystick(string name)
        {
            _joystickProviders.Remove(name);
        }
        public void RemoveEventHandler(string name)
        {
            _eventHandlerProviders.Remove(name);
        }
        #endregion
        
        #region IInputProvider
        public void UpdateInputValues()
        {
            foreach(var subsystem in _subsystemProviders.Items)
            {
                subsystem.UpdateInputValues();
            }
        }
        #endregion

        #region ICompoundProvider
        IObservableCollection ICompoundProvider.Children
        {
            get
            {
                return Children;
            }
        }
        public IObservableCollection<IProvider> Children
        {
            get
            {
                return _children;
            }
        }
        #endregion

        #region IHierarchialAccessRoot
        /// <summary>
        /// Retrieves the value at the given key
        /// </summary>
        /// <param name="key">the key to look for the value at</param>
        /// <returns>the value at <paramref name="key"/></returns>
        public async Task<string> GetAsync(string key)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWaitAsync(key, "", true);
            //throw any applicable exceptions
            CheckExceptions(ticket);
            //get the result
            var ret = _hierarchialAccessResponses[ticket];
            //cleanup the ticket
            CleanupTicket(ticket);
            return ret;
        }
        /// <summary>
        /// Sets the value at the given key
        /// </summary>
        /// <param name="key">where to set <paramref name="value"/> at</param>
        /// <param name="value">the value to set at location <paramref name="key"/></param>
        /// <param name="safe">if true, then the task waits for the result and throws any exceptions that occured; if false, then just send the request and leave</param>
        /// <returns></returns>
        public async Task SetAsync(string key, string value, bool safe)
        {
            if(safe)
            {
                //wait for the request to complete
                long ticket = await EnqueueAndWaitAsync(key, value, true);
                //throw any applicable exceptions
                CheckExceptions(ticket);
                //cleanup the ticket
                CleanupTicket(ticket);
            }
            else
            {
                //wait for the request to complete
                Enqueue(key, value, false);
            }
        }
        /// <summary>
        /// Used to determine whether an item exsits at the given key
        /// </summary>
        /// <param name="key">the key to check</param>
        /// <returns>whether or not an item exists at <paramref name="key"/></returns>
        bool IHierarchialAccessRoot.KeyExists(string key)
        {
            return base.KeyExists(key);
        }

        private async Task<long> EnqueueAndWaitAsync(string path, string value, bool read)
        {
            //get the next ticket
            long ticket = GetNextTicketNumber();
            //queue the request
            _hierarchialAccessRequests.Enqueue(new Tuple<string, string, bool, long>(path, value, read, ticket));
            //wait for it to be done
            await WaitForTicket(ticket, -1);
            return ticket;
        }
        private void Enqueue(string path, string value, bool read)
        {
            //queue our request; don't bother getting a ticket number because the caller of this method is just going to return
            _hierarchialAccessRequests.Enqueue(new Tuple<string, string, bool, long>(path, value, read, -1));
        }
        private void CheckExceptions(long ticket)
        {
            //get any exception info
            if (_hierarchialAccessExceptions.ContainsKey(ticket))
            {
                try
                {
                    throw _hierarchialAccessExceptions[ticket];
                }
                finally
                {
                    //if there is an exception, always cleanup the exception
                    CleanupTicket(ticket);
                }
            }
        }
        private void CleanupTicket(long ticket)
        {
            _hierarchialAccessDeadTickets.Add(ticket);
        }
        private async Task WaitForTicket(long ticket, int timeoutMs)
        {
            //TODO: how fast is this?
            int timeoutCycleDuration = timeoutMs / 10;

            //-1 is indefinite
            for(int i = 0; i < timeoutMs || timeoutMs == -1; i += timeoutCycleDuration)
            {
                if (_hierarchialAccessResponses.ContainsKey(ticket))
                    return;
                await Task.Delay(timeoutCycleDuration);
            }
        }
        private long GetNextTicketNumber()
        {
            return Interlocked.Increment(ref _ticketCounter);
        }
        private long _ticketCounter = 0;
        
        private ConcurrentQueue<Tuple<string, string, bool, long>> _hierarchialAccessRequests = new ConcurrentQueue<Tuple<string, string, bool, long>>();
        private ConcurrentDictionary<long, string> _hierarchialAccessResponses = new ConcurrentDictionary<long, string>();
        private ConcurrentDictionary<long, Exception> _hierarchialAccessExceptions = new ConcurrentDictionary<long, Exception>();
        private ConcurrentBag<long> _hierarchialAccessDeadTickets = new ConcurrentBag<long>();
        private int _activeGetRequests = 0;

        //TODO: how do I make this stop when the object is destroyed; should i use IDisposable?
        private void WorkerThreadMethod()
        {
            //this must not throw any exceptions
            
            CancellationToken workerMethodToken = _hierarchialAccesCTS.Token;

            Tuple<string, string, bool, long> processItem;
            Tuple<string, string, bool, long> peakNext;
            List<Tuple<string, string, bool, long>> itemQueue = new List<Tuple<string, string, bool, long>>();
            List<Task> processes = new List<Task>();

            //process the queue until someone tells us otherwise
            while (!workerMethodToken.IsCancellationRequested)
            {
                //always work unless there are no requests
                //get the next reqeust
                while (_hierarchialAccessRequests.TryDequeue(out processItem))
                {
                    try
                    {
                        //is this a read or a write request?
                        if (processItem.Item3)
                        {
                            //read request
                            
                            //for some reason the first request is 10s
                            //Interlocked.Increment(ref _activeGetRequests);
                            //GetWorkerAsync(processItem.Item1, processItem.Item4);

                            //TODO: this might be able to be condensed into a single line
                            var result = this[processItem.Item1];
                                _hierarchialAccessResponses[processItem.Item4] = result;
                        }
                        else
                        {
                            //wait for the other requests to be done
                            while (_activeGetRequests != 0) ;
                            //write request
                            this[processItem.Item1] = processItem.Item2;
                            if (processItem.Item4 != -1)
                                _hierarchialAccessResponses[processItem.Item4] = "";
                        }
                    }
                    catch (Exception e)
                    {
                        //make sure exceptions are handled
                        if (processItem.Item4 != -1)
                        {
                            _hierarchialAccessExceptions[processItem.Item4] = e;
                            _hierarchialAccessResponses[processItem.Item4] = "";//make sure this is created so we don't get hung up waiting for something that will never happen
                        }
                    }                    
                }
            }
        }
        private Task GetWorkerAsync(string path, long ticket)
        {
            return Task.Run(() =>
            {
                //actually make the get request
                _hierarchialAccessResponses[ticket] = this[path];
                //once we are done, decrement the request
                Interlocked.Decrement(ref _activeGetRequests);
            });
        }
        private void DeadTicketCleanup()
        {
            CancellationToken token = _hierarchialAccesCTS.Token;
            //every 5 seconds, clear the dead tickets
            while (!token.IsCancellationRequested)
            {
                //this is the way to remove items from the bag, becuas this does not lock up the bag, and how long this runs does not matter
                _hierarchialAccessDeadTickets.TakeWhile(
                    (long val) => { return true; });
                Task.Delay(5000).Wait();//wait 5 seconds
            }
        }
        #endregion

        #region IProvider
        public string Name
        {
            get
            {
                return "Robot";
            }
        }
        #endregion

        #region ViewModelBase
        protected override List<string> GetOverrideKeys()
        {
            return _keys;
        }
        protected override string GetValue(string key)
        {
            switch(key)
            {
                case "AnalogInputSampleRate":
                    return AnalogInputSampleRate.ToString();
                case "TeamNumber":
                    return TeamNumber.ToString();

                case "Joysticks":
                    return _joystickProviders.GetModelJson();
                case "Name":
                    return Name;
                case "EventHandlers":
                    return _eventHandlerProviders.GetModelJson();
                case "Subsystems":
                    return _subsystemProviders.GetModelJson();
                case "RobotMap":
                    return _robotMapProvider.GetModelJson();
                case "":
                    return GetModelJson();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "AnalogInputSampleRate":
                    AnalogInputSampleRate = SafeCastInt(value);
                    break;
                case "TeamNumber":
                    TeamNumber = SafeCastInt(value);
                    break;

                case "Joysticks":
                    _joystickProviders.SetModelJson(value);
                    break;
                case "EventHandlers":
                    _eventHandlerProviders.SetModelJson(value);
                    break;
                case "Subsystems":
                    _subsystemProviders.SetModelJson(value);
                    break;
                case "RobotMap":
                    _robotMapProvider.SetModelJson(value);
                    break;
                case "":
                    SetModelJson(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }
            
        }
        protected override void OnModelChange()
        {
            //setup the new providers
            if (null != ModelReference.Subsystem)
            {
                _subsystemProviders.ModelReference = ModelReference.Subsystem;
            }
            if (null != ModelReference.EventHandler)
            {
                _eventHandlerProviders.ModelReference = ModelReference.EventHandler;
            }
            if (null != ModelReference.Joystick)
            {
                _joystickProviders.ModelReference = ModelReference.Joystick;
            }
            if (null != ModelReference.RobotMap)
            {
                _robotMapProvider = new RobotMapViewModel(this);
                _robotMapProvider.ModelReference = ModelReference.RobotMap;
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _hierarchialAccesCTS.Cancel();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ViewModelBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Methods
        private void AddSubsystem(Subsystem subsystem, bool addToModel)
        {
            if (subsystem == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                ModelReference.Subsystem.Add(subsystem);

            //var provider = new SubsystemViewModel(this);
            //provider.ModelReference = subsystem;
            //provider.Name = _subsystemProviders.GetUnusedKey(provider.Name);
            //_subsystemProviders.Items.Add(provider);
            _subsystemProviders.AddOrUpdate(subsystem);
        }
        private void AddJoystick(Joystick joystick, bool addToModel)
        {
            if (joystick == null)
                throw new ArgumentNullException("joystick");
            if (addToModel)
                ModelReference.Joystick.Add(joystick);

            //var provider = new JoystickViewModel(this);
            //provider.ModelReference = joystick;
            //provider.Name = _joystickProviders.GetUnusedKey(provider.Name);
            //_joystickProviders.Items.Add(provider);
            _joystickProviders.AddOrUpdate(joystick);
        }
        private void AddEventHandler(Models.EventHandler eventHandler, bool addToModel)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                ModelReference.EventHandler.Add(eventHandler);

            //var provider = new EventHandlerViewModel(this);
            //provider.ModelReference = eventHandler;
            //provider.Name = _eventHandlerProviders.GetUnusedKey(provider.Name);
            //_eventHandlerProviders.Items.Add(provider);
            _eventHandlerProviders.AddOrUpdate(eventHandler);
        }
        
        #endregion

        #region Private Fields
        CancellationTokenSource _hierarchialAccesCTS;
        Task _hierarchialAccessTask;
        Task _hierarchailAccessDeadTokenCleanupTask;
        ObservableCollection<IProvider> _children = new ObservableCollection<IProvider>() { null, null, null, null };
        readonly List<string> _keys = new List<string>(){ "AnalogInputSampleRate", "EventHandlers", "Joysticks", "RobotMap", "Name", "OnChangeEventHandlers","OnWithinRangeEventHandlers","Subsystems","TeamNumber" };
        IRobotMapProvider _robotMapProvider
        {
            get
            {
                return _children[0] as IRobotMapProvider;
            }
            
            set
            {
                _children[0] = value;
            }
        }
        CompoundProviderList<ISubsystemProvider, Subsystem> _subsystemProviders
        {
            get
            {
                return _children[1] as CompoundProviderList<ISubsystemProvider, Subsystem>;
            }

            set
            {
                _children[1] = value;
            }
        }
        CompoundProviderList<IEventHandlerProvider, Models.EventHandler> _eventHandlerProviders
        {
            get
            {
                return _children[2] as CompoundProviderList<IEventHandlerProvider, Models.EventHandler>;
            }

            set
            {
                _children[2] = value;
            }
        }
        CompoundProviderList<IJoystickProvider, Joystick> _joystickProviders
        {
            get
            {
                return _children[3] as CompoundProviderList<IJoystickProvider, Joystick>;
            }

            set
            {
                _children[3] = value;
            }
        }
        #endregion
    }
}
