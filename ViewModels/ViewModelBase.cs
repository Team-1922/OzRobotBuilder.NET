using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Models.XML;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// This is like <see cref="KeyValuePair{TKey, TValue}"/>, but the key is readonly and the value is read/write
    /// </summary>
    public class VMKeyValuePair : IDataErrorInfo, INotifyPropertyChanged
    {
        public VMKeyValuePair(string key, ViewModelBase vm)
        {
            Set(key, vm);
        }
        public void Set(string key, ViewModelBase vm)
        {
            if (vm == null)
                throw new ArgumentNullException("vm", "ViewModel on VMKeyValuePair must not be null");
            _vm = vm;
            Key = key;
            _vm.PropertyChanged += _vm_PropertyChanged;
        }

        private void _vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == Key)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }

        ViewModelBase _vm;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; private set; }
        public string Value
        {
            get
            {
                return _vm[Key];
            }
            set
            {
                _vm[Key] = value;
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return (_vm as IDataErrorInfo)[Key];
            }
        }
    }

    public class VMKeyValueList : ObservableCollection<VMKeyValuePair>
    {
    }

    /// <summary>
    /// This wraps the <see cref="BindableBase"/>, and the ability to access values based on a string key along
    /// with enumerate through it with read AND write access to the value
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IHierarchialAccess, IDataErrorInfo, IProvider
    {
        private List<string> _keys = new List<string>();
        VMKeyValueList _keyValueList = new VMKeyValueList();
        //private int _enumeratorIndex = -1;

        private IProvider _parent;
        protected IFacet pathNameFacet;

        protected ViewModelBase(IProvider parent)
        {
            pathNameFacet = TypeRestrictions.GetValidationObjectFromTypeName("PathName");
            _parent = parent;
            UpdateKeyValueList();
            PropertyChanged += ViewModelBase_PropertyChanged;
            if (this is ICompoundProvider)
            {
                (this as ICompoundProvider).Children.CollectionChanged += Children_CollectionChanged;
            }
        }

        protected void UpdateKeyValueList()
        {
            _keys = GetOverrideKeys();
            _keyValueList.Clear();
            foreach(var key in _keys)
            {
                _keyValueList.Add(new VMKeyValuePair(key, this));
            }
        }
        public List<string> GetKeys()
        {
            return _keys;
        }
        public VMKeyValueList GetEditableKeyValueList()
        {
            return _keyValueList;
        }

        protected void RegisterChildEventPropagation(IEventPropagator child)
        {
            child.Propagated += Child_Propagated;
        }

        protected void RegisterChildrenEventPropagation()
        {
            var compoundProvider = this as ICompoundProvider;
            if (null == compoundProvider)
                return;
            foreach(IEventPropagator child in compoundProvider.Children)
            {
                if(child != null)
                    RegisterChildEventPropagation(child);
            }
        }

        #region IProvider
        public IHierarchialAccess TopParent
        {
            get
            {
                return Parent?.TopParent ?? this;
            }
        }
        public IProvider Parent
        {
            get
            {
                return _parent;
            }
        }
        public string FullyQualifiedName
        {
            get
            {
                return Parent != null ? $"{Parent.FullyQualifiedName}.{Name}" : $"{Name}";
            }
        }
        public abstract string Name { get; set; }
        private string _modelJson = "";
        public string GetModelJson()
        {
            if(_modelJson == "")
                return _modelJson = JsonSerialize(ModelReference);
            return _modelJson;
        }
        public void SetModelJson(string text)
        {
            try
            {
                //deserialize the model
                ModelReference = JsonDeserialize(text);
                _modelJson = text;
            }
            catch (Exception e)
            {
                //this means an exception was thrown while loading (whether bad json, OR type validation)
                throw new ArgumentException("Invalid Json", "text");
            }
        }
        public override string ToString()
        {
            return GetModelJson();
        }
        private void InvalidateModelJson()
        {
            _modelJson = "";
        }
        private object _modelReference;
        public object ModelReference
        {
            get
            {
                return _modelReference;
            }

            set
            {
                SetProperty(ref _modelReference, value);
            }
        }
        public string ModelTypeName
        {
            get
            {
                var brokenName = ModelReference.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }
        #endregion

        #region IHierarchialAccess Methods
        /// <summary>
        /// Hierarchial exception-safe access to data
        /// </summary>
        /// <param name="key">the path to the value to read</param>
        /// <param name="value">the value read from <paramref name="key"/></param>
        /// <returns>whether or not the read was successful</returns>
        public bool TryGetValue(string key, out string value)
        {
            try
            {
                value = this[key];
            }
            catch (Exception)
            {
                value = "";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Hierarchial exception-safe access to data
        /// </summary>
        /// <param name="key">the path to the value to write</param>
        /// <param name="value">the value to write to<paramref name="key"/></param>
        /// <returns>whether or not the write was successful</returns>
        public bool TrySetValue(string key, string value)
        {
            try
            {
                this[key] = value;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Checks to see whether a given key exists
        /// </summary>
        /// <param name="key">the item to check</param>
        /// <returns>whether or not an item exists at <paramref name="key"/></returns>
        public bool KeyExists(string key)
        {
            //make sure this is a valid path
            TypeRestrictions.Validate(pathNameFacet, key);

            return _keyExists(key);
        }
        private bool _keyExists(string key)
        {
            //NOTE: this is VERY similar to the behavior of ValueReadWrite

            var thisMember = key.Split(new char[] { '.' }, 2, StringSplitOptions.None);
            if (null == thisMember)
                throw new ArgumentException($"\"{key}\" Is an Invalid Property");
            else if (thisMember.Length == 1)
            {
                return _keys.Contains(thisMember[0]);
            }

            //while it might make sense to do this hierarchially, putting this code here means any new additions to provider interfaces only need to change here
            //  and not every time they would be accessed in the other view-models

            //if this is a compound provider, loop through all of its sub-view-models
            if (this is ICompoundProvider)
            {
                var me = this as ICompoundProvider;
                foreach (IProvider child in me.Children)
                {
                    if (child.Name == thisMember[0])
                    {
                        //if this is also a hierarchial access, which is almost definitely is, then call the child's function
                        if (child is IHierarchialAccess)
                        {
                            return (child as IHierarchialAccess).KeyExists(thisMember[1]);
                        }
                    }
                }
            }
            throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
        }

        /// <summary>
        /// This gives read/write access to the viewmodel based on the name of the property;
        /// NOTE: this does support hierarchial access
        /// </summary>
        /// <param name="key">The name of the property</param>
        /// <returns>the string representation of the property</returns>
        public string this[string key]
        {
            get
            {
                //make sure this is a valid path
                TypeRestrictions.Validate(pathNameFacet, key);
                return ValueReadWrite(key, true);
            }

            set
            {
                //make sure this is a valid path
                TypeRestrictions.Validate(pathNameFacet, key);
                ValueReadWrite(key, false, value);
            }
        }
        /// <summary>
        /// This is a helper method for <see cref="this[string]"/>, becuase the recursive access
        /// shares most of the code between read and write
        /// </summary>
        /// <param name="key">the name of the variable to modify</param>
        /// <param name="read"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ValueReadWrite(string key, bool read, string value="")
        {
            //if this is blank, just return this; this only happens on the top-level node
            if (key == "")
            {
                if (read)
                    return GetModelJson();
                else
                    SetModelJson(value);
            }

            var thisMember = key.Split(new char[] { '.' }, 2, StringSplitOptions.None);
            if (null == thisMember)
                throw new ArgumentException($"\"{key}\" Is an Invalid Property");
            else if (thisMember.Length == 1)
            {
                if (read)
                    return GetValue(key) ?? "";
                else
                    SetValue(key, value);
                return "";
            }

            //while it might make sense to do this hierarchially, putting this code here means any new additions to provider interfaces only need to change here
            //  and not every time they would be accessed in the other view-models

            //if this is a compound provider, loop through all of its sub-view-models
            if (this is ICompoundProvider)
            {
                var me = this as ICompoundProvider;
                foreach (IProvider child in me.Children)
                {
                    if (child.Name == thisMember[0])
                    {
                        if (child is ViewModelBase)
                        {
                            return (child as ViewModelBase).ValueReadWrite(thisMember[1], read, value);
                        }
                        else
                        {
                            if (read)
                            {
                                return child[thisMember[1]];
                            }
                            else
                            {
                                child[thisMember[1]] = value;
                                return "";
                            }
                        }

                    }
                }
            }
            throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
        }
        #endregion

        #region Child Casting Helper Methods
        protected int SafeCastInt(string value)
        {
            int ret;
            bool success = int.TryParse(value, out ret);
            if (success)
                return ret;
            else
                throw new ArgumentException("Value Entered Not Integer");
        }
        protected long SafeCastLong(string value)
        {
            long ret;
            bool success = long.TryParse(value, out ret);
            if (success)
                return ret;
            else
                throw new ArgumentException("Value Entered Not Long");
        }
        protected double SafeCastDouble(string value)
        {
            double ret;
            bool success = double.TryParse(value, out ret);
            if (success)
                return ret;
            else
                throw new ArgumentException("Value Entered Not Double");
        }
        protected T SafeCastEnum<T>(string value) where T : struct
        {
            T ret;
            bool success = Enum.TryParse(value, true, out ret);
            if (success)
                return ret;
            else
                throw new ArgumentException($"Value Entered Not Compatable With {typeof(T).ToString()}");
        }
        protected bool SafeCastBool(string value)
        {
            bool ret;
            bool success = bool.TryParse(value, out ret);
            if (success)
                return ret;
            else
                throw new ArgumentException("Value Entered Not Boolean");
        }
        protected string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }
        protected T JsonDeserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text, Settings);
        }
        protected object JsonDeserialize(string text)
        {
            return JsonConvert.DeserializeObject(text, Settings);
        }
        #endregion

        #region Child Helper Methods
        protected bool ContainsDescendentNamed(string name)
        {
            if(this is ICompoundProvider)
            {
                var thisCompoundProvider = this as ICompoundProvider;
                foreach(IProvider child in thisCompoundProvider.Children)
                {
                    if (child.Name == name)
                        return true;
                    if(child is ICompoundProvider && child is ViewModelBase)
                    {
                        var splitName = name.Split(new char[] { '.' }, 2, StringSplitOptions.None);
                        if (splitName.Length == 1 || splitName.Length == 0)
                            return false;
                        return (child as ViewModelBase).ContainsDescendentNamed(splitName[1]);
                    }
                }
            }
            return false;
        }
        #endregion

        #region Abstract & Virtual Methods
        /// <summary>
        /// This gives read access to the viewmodel based on the name of the property; this should not give hierarchial access
        /// </summary>
        /// <param name="key">the name of the property</param>
        /// <returns>the string representation of the property</returns>
        protected abstract string GetValue(string key);
        /// <summary>
        /// This gives write access to the viewmodel based on the name of the property; this should not give herarchial access
        /// </summary>
        /// <param name="key">the name of the property</param>
        /// <param name="value">the string representation of the property</param>
        protected abstract void SetValue(string key, string value);
        protected virtual List<string> GetOverrideKeys()
        {
            return (from x in GetType().GetProperties()
                    where !IsOnNameBlacklist(x.Name)
                    select x.Name).ToList();
        }
        /// <summary>
        /// In order for IDataErrorInfo to work correctly, we need the name of model type to lookup in the type restrictions
        /// </summary>
        /// <returns>The name of the model type without the namespace (i.e. "Robot")</returns>
        //public abstract string ModelTypeName { get; }
        /// <summary>
        /// This allows the consumer to define custom methods of getting an error string.  If this is not overridden,
        /// then the regular TypeRestriction method is used
        /// </summary>
        /// <param name="attribName">the name of the attribute to check; NOTE: the class name is not included in "attribName"</param>
        /// <returns>the error string</returns>
        protected virtual string GetErrorString(string attribName)
        {
            return TypeRestrictions.DataErrorString($"{ModelTypeName}.{attribName}", this[attribName]);
        }
        protected virtual void OnModelChange() { }
        #endregion

        #region IDataErrorInfo Support
        Dictionary<string, string> _errorInfoMap = new Dictionary<string, string>();
        string IDataErrorInfo.Error
        {
            get
            {
                string ret = "";
                foreach(var pair in _errorInfoMap)
                {
                    if(pair.Value != "")
                        ret += $"{pair.Value}{Environment.NewLine}";
                }
                return ret;
            }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return _errorInfoMap[columnName] = GetErrorString(columnName);
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
            InvalidateModelJson();
            if (null == ModelReference)
                return;

            //add our name to the name; if this is the top-level, then don't add our name so the IHierarchialAccess works correctly
            e.PropertyName = Parent != null ? e.PropertyName == "" ? Name : $"{Name}.{e.PropertyName}" : e.PropertyName;
            _propagatedNoDuplicates?.Invoke(e);
        }
        #endregion

        #region Private Methods
        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateModelJson();
            if (e.PropertyName == "ModelReference")
                OnModelChange();            
        }
        #endregion

        #region Event Propagaiton
        private void Child_Propagated(EventPropagationEventArgs e)
        {
            OnEventPropagated(e);
        }
        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RegisterChildrenEventPropagation();

            //propagate this whole object to be reuploaded (potentially a better way to do this would be with individual delete/post requests
            OnEventPropagated(new EventPropagationEventArgs("PUT", "", ToString()));
        }
        #endregion

        #region BindableBase        
        protected override bool SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;
            item = value;
            OnPropertyChanged(propertyName);
            OnEventPropagated(new EventPropagationEventArgs("PUT", propertyName, value.ToString()));
            return true;
        }
        #endregion

        /// <summary>
        /// Checks to see whether the given property name is on the blacklist of internal property names
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsOnNameBlacklist(string name)
        {
            return name == "Properties"
                       || name == "Children"
                       || name == "this[string]"
                       || name == "Current"
                       || name == "Item" //This is a weird one.  All of them seem to have it with the one exception of the "RobotViewModelBase"
                       || name == "Count"
                       || name == "IsReadOnly"
                       || name == "ModelTypeName"
                       || name == "TopParent"
                       || name == "Parent"
                       || name == "ModelReference";
        }
    }

    public abstract class ViewModelBase<ModelType> : ViewModelBase
    {
        public ViewModelBase(IProvider parent) : base(parent)
        {
        }

        public virtual new ModelType ModelReference
        {
            get
            {
                return (ModelType)base.ModelReference;
            }

            set
            {
                base.ModelReference = value;
            }
        }
    }

}
