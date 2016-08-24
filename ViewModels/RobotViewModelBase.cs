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
            for (int i = 0; i < _subsystemProviders.Items.Count; ++i)
            {
                if (_subsystemProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _subsystemProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.Subsystem.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveJoystick(string name)
        {
            for (int i = 0; i < _joystickProviders.Items.Count; ++i)
            {
                if (_joystickProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _joystickProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.Joystick.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveEventHandler(string name)
        {
            for (int i = 0; i < _eventHandlerProviders.Items.Count; ++i)
            {
                if (_eventHandlerProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _eventHandlerProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.EventHandler.RemoveAt(i);
                    break;
                }
            }
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
        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
            }
        }
        #endregion

        #region IHierarchialAccessRoot
        public async Task<string> GetAsync(string path)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWait(path, "", true);
            //throw any applicable exceptions
            CheckExceptions(ticket);
            //get the result
            var ret = _hierarchialAccessResponses[ticket];
            //cleanup the ticket
            CleanupTicket(ticket);
            return ret;
        }
        public async Task SetAsync(string path, string value)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWait(path, value, false);
            //throw any applicable exceptions
            CheckExceptions(ticket);
            //cleanup the ticket
            CleanupTicket(ticket);
        }
        private async Task<long> EnqueueAndWait(string path, string value, bool read)
        {
            //get our ticket number
            long ticket = GetNextTicketNumber();
            //queue our request
            _hierarchialAccessRequests.Enqueue(new Tuple<string, string, bool, long>(path, value, read, ticket));
            //wait for our ticket to be done
            await WaitForTicket(ticket, 10);//TODO: what should the timeout be?
            return ticket;
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
            for(int i = 0; i < timeoutMs; i += timeoutCycleDuration)
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

        //TODO: how do I make this stop when the object is destroyed; should i use IDisposable?
        private void WorkerThreadMethod()
        {
            //this must not throw any exceptions
            
            CancellationToken workerMethodToken = _hierarchialAccesCTS.Token;
            //process the queue until someone tells us otherwise
            while (!workerMethodToken.IsCancellationRequested)
            {
                //always work unless there are no requests
                while (_hierarchialAccessRequests.Count != 0)
                {
                    //get the next reqeust
                    Tuple<string, string, bool, long> processItem;
                    if(_hierarchialAccessRequests.TryDequeue(out processItem))
                    {
                        try
                        {
                            //is this a read or a write request?
                            if (processItem.Item3)
                            {
                                //read request

                                //TODO: this might be able to be condensed into a single line
                                var result = this[processItem.Item1];
                                _hierarchialAccessResponses[processItem.Item4] = result;
                            }
                            else
                            {
                                //write request
                                this[processItem.Item1] = processItem.Item2;
                            }
                        }
                        catch (Exception e)
                        {
                            //make sure exceptions are handled
                            _hierarchialAccessExceptions[processItem.Item4] = e;
                        }
                    }
                }
            }
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

            var provider = new SubsystemViewModel(this);
            provider.ModelReference = subsystem;
            provider.Name = _subsystemProviders.GetUnusedKey(provider.Name);
            _subsystemProviders.Items.Add(provider);
        }
        private void AddJoystick(Joystick joystick, bool addToModel)
        {
            if (joystick == null)
                throw new ArgumentNullException("joystick");
            if (addToModel)
                ModelReference.Joystick.Add(joystick);

            var provider = new JoystickViewModel(this);
            provider.ModelReference = joystick;
            provider.Name = _joystickProviders.GetUnusedKey(provider.Name);
            _joystickProviders.Items.Add(provider);
        }
        private void AddEventHandler(Models.EventHandler eventHandler, bool addToModel)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                ModelReference.EventHandler.Add(eventHandler);

            var provider = new EventHandlerViewModel(this);
            provider.ModelReference = eventHandler;
            provider.Name = _eventHandlerProviders.GetUnusedKey(provider.Name);
            _eventHandlerProviders.Items.Add(provider);
        }
        #endregion

        #region Private Fields
        CancellationTokenSource _hierarchialAccesCTS;
        Task _hierarchialAccessTask;
        Task _hierarchailAccessDeadTokenCleanupTask;
        Dictionary<string, IProvider> _children = new Dictionary<string, IProvider>();
        readonly List<string> _keys = new List<string>(){ "AnalogInputSampleRate", "EventHandlers", "Joysticks", "RobotMap", "Name", "OnChangeEventHandlers","OnWithinRangeEventHandlers","Subsystems","TeamNumber" };
        IRobotMapProvider _robotMapProvider
        {
            get
            {
                return _children["_robotMapProvider"] as IRobotMapProvider;
            }
            
            set
            {
                _children["_robotMapProvider"] = value;
            }
        }
        CompoundProviderList<ISubsystemProvider, Subsystem> _subsystemProviders
        {
            get
            {
                return _children["_subsystemProviders"] as CompoundProviderList<ISubsystemProvider, Subsystem>;
            }

            set
            {
                _children["_subsystemProviders"] = value;
            }
        }
        CompoundProviderList<IEventHandlerProvider, Models.EventHandler> _eventHandlerProviders
        {
            get
            {
                return _children["_eventHandlerProviders"] as CompoundProviderList<IEventHandlerProvider, Models.EventHandler>;
            }

            set
            {
                _children["_eventHandlerProviders"] = value;
            }
        }
        CompoundProviderList<IJoystickProvider, Joystick> _joystickProviders
        {
            get
            {
                return _children["_joystickProviders"] as CompoundProviderList<IJoystickProvider, Joystick>;
            }

            set
            {
                _children["_joystickProviders"] = value;
            }
        }
        #endregion
    }
}
