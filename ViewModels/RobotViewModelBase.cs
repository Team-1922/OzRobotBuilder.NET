﻿using System;
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
    public class RobotViewModelBase : CompoundViewModelBase<Robot>, IRobotProvider, IDisposable
    {
        public RobotViewModelBase() : base(null)
        {
            _subsystemProviders = new CompoundProviderList<ISubsystemProvider, Subsystem>("Subsystems", this, (Subsystem model) => { return new SubsystemViewModel(_subsystemProviders) { ModelReference = model }; });
            _eventHandlerProviders = new CompoundProviderList<IEventHandlerProvider, Models.EventHandler>("EventHandlers", this, (Models.EventHandler model) => { return new EventHandlerViewModel(_eventHandlerProviders) { ModelReference = model }; });
            _joystickProviders = new CompoundProviderList<IJoystickProvider, Joystick>("Joysticks", this, (Joystick model) => { return new JoystickViewModel(_joystickProviders) { ModelReference = model }; });

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
                var temp = ModelReference.TeamNumber;
                SetProperty(ref temp, value);
                ModelReference.TeamNumber = temp;
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
                var temp = ModelReference.AnalogInputSampleRate;
                SetProperty(ref temp, value);
                ModelReference.AnalogInputSampleRate = temp;
            }
        }
        public int ModifyingPortNumber
        {
            get
            {
                return ModelReference.ModifyPortNumber;
            }

            set
            {
                var temp = ModelReference.ModifyPortNumber;
                SetProperty(ref temp, value);
                ModelReference.ModifyPortNumber = temp;
            }
        }
        public int UpdatesPortNumber
        {
            get
            {
                return ModelReference.UpdatesPortNumber;
            }

            set
            {
                var temp = ModelReference.UpdatesPortNumber;
                SetProperty(ref temp, value);
                ModelReference.UpdatesPortNumber = temp;
            }
        }
        public void RemoveSubsystem(string name)
        {
            TopParent.SetAsync($"{_subsystemProviders.Name}.name", null).Wait();
        }
        public void RemoveJoystick(string name)
        {
            TopParent.SetAsync($"{_joystickProviders.Name}.name", null).Wait();
        }
        public void RemoveEventHandler(string name)
        {
            TopParent.SetAsync($"{_eventHandlerProviders.Name}.name", null).Wait();
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
        public override IObservableCollection Children
        {
            get
            {
                return _children;
            }
        }
        #endregion

        #region IHierarchialAccessRoot
        #region Public Methods
        /// <summary>
        /// Retrieves the value at the given key
        /// </summary>
        /// <param name="key">the key to look for the value at</param>
        /// <returns>the value at <paramref name="key"/></returns>
        public async Task<string> GetAsync(string key)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWaitAsync(key, "", AccessType.Get);
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
        /// <param name="propagate">whether or not to propagate this request</param>
        /// <returns></returns>
        public async Task SetAsync(string key, string value, bool safe = true, bool propagate = true)
        {
            if (safe)
            {
                //wait for the request to complete
                long ticket = await EnqueueAndWaitAsync(key, value, AccessType.Set);
                //throw any applicable exceptions
                CheckExceptions(ticket);
                //cleanup the ticket
                CleanupTicket(ticket);
                //propagate this event if enabled
                if (propagate)
                    OnEventPropagated(new EventPropagationEventArgs(Protocall.Method.Set, key, value));
            }
            else
            {
                //don't wait for the request to complete (also don't propagate)
                Enqueue(key, value, AccessType.Set);
            }
        }
        public async Task<bool> DeleteAsync(string key, bool propagate = true)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWaitAsync(key, "", AccessType.Delete);
            //throw any applicable exceptions
            CheckExceptions(ticket);
            //get the result
            var ret = _hierarchialAccessResponses[ticket];
            //cleanup the ticket
            CleanupTicket(ticket);
            //propagate this event if enabled
            if (propagate)
                OnEventPropagated(new EventPropagationEventArgs(Protocall.Method.Delete, key, ""));
            return ret == "deleted";
        }
        public async Task<bool> AddAsync(string key, string value, bool propagate = true)
        {
            //wait for the request to complete
            long ticket = await EnqueueAndWaitAsync(key, "", AccessType.Add);
            //throw any applicable exceptions
            CheckExceptions(ticket);
            //get the result
            var ret = _hierarchialAccessResponses[ticket];
            //cleanup the ticket
            CleanupTicket(ticket);
            //propagate this event if enabled
            if (propagate)
                OnEventPropagated(new EventPropagationEventArgs(Protocall.Method.Add, key, value));
            return ret == "added";
        }
        /// <summary>
        /// Used to determine whether an item exsits at the given key
        /// </summary>
        /// <param name="key">the key to check</param>
        /// <returns>whether or not an item exists at <paramref name="key"/></returns>
        bool IHierarchialAccessRoot.KeyExists(string key)
        {
            //TODO: optimize using our lookup table
            return base.KeyExists(key);
        }
        /// <summary>
        /// This is called when the tree is updated (viewmodel objects added/deleted) to create a look-up table for the entire tree
        /// in order to optimize read/write performnace.  This is typically called once at the beginning of a non-debug application run
        /// </summary>
        public void PrecomputeTree()
        {
            //if we are precomputing the tree, make sure we re-compute the tree when there is a relevent change
            Propagated += RobotViewModelBase_Propagated;
            //iterate through the tree
            _lookupTableMutex.EnterWriteLock();
            
            try
            {
                IterateTreeInternal("", this);
            }
            finally
            {
                _lookupTableMutex.ExitWriteLock();
            }
        }
        private void IterateTreeInternal(string path, ICompoundProvider provider)
        {
            string name;
            foreach (IProvider child in provider.Children)
            {
                name = path == "" ? $"{child.Name}" : $"{path}.{child.Name}";
                if (child is ICompoundProvider)
                {
                    IterateTreeInternal(name, child as ICompoundProvider);
                }
                if (child is IHierarchialAccess)
                {
                    _treeReferenceLookupTable.Add(name, child as IHierarchialAccess);
                }
            }
        }
        #endregion

        #region Helper Methods to Public Methods
        private async Task<long> EnqueueAndWaitAsync(string path, string value, AccessType method)
        {
            //get the next ticket
            long ticket = GetNextTicketNumber();
            //queue the request
            _hierarchialAccessRequests.Enqueue(new Tuple<string, string, AccessType, long>(path, value, method, ticket));
            _containsNewQueueItems = true;
            //wait for it to be done
            await WaitForTicket(ticket, -1);
            return ticket;
        }
        private void Enqueue(string path, string value, AccessType method)
        {
            //queue our request; don't bother getting a ticket number because the caller of this method is just going to return
            _hierarchialAccessRequests.Enqueue(new Tuple<string, string, AccessType, long>(path, value, method, -1));
            _containsNewQueueItems = true;
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
        #endregion

        #region Fields
        private ConcurrentQueue<Tuple<string, string, AccessType, long>> _hierarchialAccessRequests = new ConcurrentQueue<Tuple<string, string, AccessType, long>>();
        private ConcurrentDictionary<long, string> _hierarchialAccessResponses = new ConcurrentDictionary<long, string>();
        private ConcurrentDictionary<long, Exception> _hierarchialAccessExceptions = new ConcurrentDictionary<long, Exception>();
        private ConcurrentBag<long> _hierarchialAccessDeadTickets = new ConcurrentBag<long>();
        private Dictionary<string, IHierarchialAccess> _treeReferenceLookupTable = new Dictionary<string, IHierarchialAccess>();
        private ReaderWriterLockSlim _lookupTableMutex = new ReaderWriterLockSlim();
        private volatile int _activeGetRequests = 0;
        private long _ticketCounter = 0;
        private volatile bool _containsNewQueueItems = false;
        #endregion

        #region Utilities
        private enum AccessType
        {
            Get,
            Set,
            Add,
            Delete
        }
        private class AccessPair
        {
            public AccessPair(IHierarchialAccess access, string path)
            {
                _path = path;
                _object = access;
            }
            private string _path;
            private IHierarchialAccess _object;
            public void Set(string value)
            {
                _object[_path] = value;
            }
            public string Get()
            {
                return _object[_path];
            }
        }
        private AccessPair GetAccessPair(string key)
        {
            IHierarchialAccess accessObject = this;
            string accessPath = key;
            _lookupTableMutex.EnterReadLock();
            try
            {
                if (_treeReferenceLookupTable.Count != 0)
                {
                    //get path of the object to lookup
                    var splitPath = key.Split('.');
                    var objPath = string.Join(".", (from token in splitPath where token != splitPath.Last() select token));
                    foreach (var pair in _treeReferenceLookupTable)
                    {
                        if (pair.Key == objPath)
                        {
                            accessObject = pair.Value;
                            accessPath = splitPath.Last();
                            break;
                        }
                    }
                }
            }
            finally
            {
                _lookupTableMutex.ExitReadLock();
            }
            return new AccessPair(accessObject, accessPath);
        }
        private void WaitWriteAccess()
        {
            //wait for the other requests to be done
            while (_activeGetRequests != 0) ;
        }
        #endregion

        #region Worker Methods
        //TODO: how do I make this stop when the object is destroyed; should i use IDisposable?
        private void WorkerThreadMethod()
        {
            //this must not throw any exceptions
            
            CancellationToken workerMethodToken = _hierarchialAccesCTS.Token;

            Tuple<string, string, AccessType, long> processItem;

            //process the queue until someone tells us otherwise
            while (!workerMethodToken.IsCancellationRequested)
            {
                if (_containsNewQueueItems)
                {
                    _containsNewQueueItems = false;
                    //always work unless there are no requests
                    //get the next reqeust
                    while (_hierarchialAccessRequests.TryDequeue(out processItem))
                    {
                        // TODO: is there any reason why this line is necessary
                        // _lookupTableMutex.EnterReadLock();
                        try
                        {
                            switch (processItem.Item3)
                            {
                                case AccessType.Get:
                                    GetOperation(processItem.Item1, processItem.Item4);
                                    break;
                                case AccessType.Set:
                                    SetOperation(processItem.Item1, processItem.Item2, processItem.Item4);
                                    break;
                                case AccessType.Add:
                                    AddOperation(processItem.Item1, processItem.Item2, processItem.Item4);
                                    break;
                                case AccessType.Delete:
                                    DeleteOperation(processItem.Item1, processItem.Item4);
                                    break;
                                default:
                                    break;
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
                        finally
                        {
                            // TODO: is there any reason why this line is necessary
                            // _lookupTableMutex.ExitReadLock();
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
        private Task GetWorkerAsync(string path, long ticket)
        {
            return Task.Run(() =>
            {
                //actually make the get request
                var accessObject = GetAccessPair(path);

                _hierarchialAccessResponses[ticket] = accessObject.Get();
                //once we are done, decrement the request
                Interlocked.Decrement(ref _activeGetRequests);
            });
        }
        #endregion

        #region AccessOperations
        private void GetOperation(string key, long ticket)
        {
            //read request

            Interlocked.Increment(ref _activeGetRequests);
            GetWorkerAsync(key, ticket);
        }
        private void SetOperation(string key, string value, long ticket)
        {
            //wait for the other requests to be done
            WaitWriteAccess();

            var accessObject = GetAccessPair(key);

            //write request
            accessObject.Set(value);
            if (ticket != -1)
                _hierarchialAccessResponses[ticket] = "";
        }
        private void AddOperation(string key, string value, long ticket)
        {
            //wait for the other requests to be done
            WaitWriteAccess();

            var accessObject = GetAccessPair(key);

            string message = "added";
            try
            {
                var result = accessObject.Get();
            }
            catch (Exception)
            {
                message = "";
            }

            //add request
            accessObject.Set(value);

            _hierarchialAccessResponses[ticket] = message;
        }
        private void DeleteOperation(string key, long ticket)
        {
            //wait for the other requests to be done
            WaitWriteAccess();

            var accessObject = GetAccessPair(key);

            //delete request
            accessObject.Set(null);

            try
            {
                var result = accessObject.Get();
            }
            catch(Exception)
            {
                _hierarchialAccessResponses[ticket] = "deleted";
            }
            _hierarchialAccessResponses[ticket] = "";
        }
        #endregion
        #endregion

        #region IProvider
        public override string Name
        {
            get
            {
                return "Robot";
            }

            set
            {
                throw new InvalidOperationException("Cannot Set Name of Robot");//TODO: maybe we should?
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
                case "ModifyingPortNumber":
                    return ModifyingPortNumber.ToString();
                case "UpdatesPortNumber":
                    return UpdatesPortNumber.ToString();

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
                case "ModifyingPortNumber":
                    ModifyingPortNumber = SafeCastInt(value);
                    break;
                case "UpdatesPortNumber":
                    ModifyingPortNumber = SafeCastInt(value);
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
        
        #region IEventPropagator
        private event EventPropagationEventHandler _propagatedNoDuplicates;

        public event EventPropagationEventHandler Propagated
        {
            add
            {
                if (null == _propagatedNoDuplicates || !_propagatedNoDuplicates.GetInvocationList().Contains(value))
                    _propagatedNoDuplicates += value;
            }

            remove
            {
                _propagatedNoDuplicates -= value;
            }
        }

        protected void OnEventPropagated(EventPropagationEventArgs e)
        {
            //InvalidateModelJson();
            if (null == ModelReference)
                return;

            //add our name to the name; if this is the top-level, then don't add our name so the IHierarchialAccess works correctly
            //e.PropertyName = Parent != null ? e.PropertyName == "" ? Name : $"{Name}.{e.PropertyName}" : e.PropertyName;
            _propagatedNoDuplicates?.Invoke(e);
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
            {
                TopParent.SetAsync($"{_subsystemProviders.Name}.{subsystem.Name}", JsonSerialize(subsystem));
                return;
                //ModelReference.Subsystem.Add(subsystem);
            }

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
            {
                TopParent.SetAsync($"{_joystickProviders.Name}.{joystick.Name}", JsonSerialize(joystick));
                return;
                //ModelReference.Joystick.Add(joystick);
            }

            //var provider = new JoystickViewModel(this);
            //provider.ModelReference = joystick;
            //provider.Name = _joystickProviders.GetUnusedKey(provider.Name);
            //_joystickProviders.Items.Add(provider);
            _joystickProviders.AddOrUpdate(joystick);
        }
        private void AddEventHandler(Models.EventHandler eventHandler, bool addToModel)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler");
            if (addToModel)
            {
                TopParent.SetAsync($"{_eventHandlerProviders.Name}.{eventHandler.Name}", JsonSerialize(eventHandler));
                return;
                //ModelReference.EventHandler.Add(eventHandler);
            }

            //var provider = new EventHandlerViewModel(this);
            //provider.ModelReference = eventHandler;
            //provider.Name = _eventHandlerProviders.GetUnusedKey(provider.Name);
            //_eventHandlerProviders.Items.Add(provider);
            _eventHandlerProviders.AddOrUpdate(eventHandler);
        }
        
        private void RobotViewModelBase_Propagated(EventPropagationEventArgs e)
        {
            //run this as a task, becuase this method is called very indirectly from the worker thread, therefore the lookup table will be on read-lock;
            //  this will result in attempting to enter a write lock while in a read lock which will cause issues
            Task.Run(() =>
            {
                if (e.Method == Protocall.Method.Set || e.Method == Protocall.Method.Add || e.Method == Protocall.Method.Delete)
                {
                    bool shouldPrecomputeTree = false;
                    _lookupTableMutex.EnterReadLock();
                    try
                    {
                        if (_treeReferenceLookupTable.Count != 0)
                        {
                            //get path of the object to lookup
                            var splitPath = e.PropertyName.Split('.');
                            if (splitPath.Last() == "Name" || ContainsDescendentNamed(e.PropertyName))
                            {
                                shouldPrecomputeTree = true;
                            }
                        }
                    }
                    finally
                    {
                        _lookupTableMutex.ExitReadLock();
                    }
                    if (shouldPrecomputeTree)
                        PrecomputeTree();
                }
            });
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
