using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class RobotMapViewModel : ViewModelBase<List<RobotMapEntry>>, IRobotMapProvider
    {
        public RobotMapViewModel(IRobotProvider parent) : base(parent)
        {
            //EventAggregator<AddRobotMapEntryEvent>.Instance.Event += OnAddNewEntry;
        }

        #region IRobotMapProvider
        public void AddEntry(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            //this means the key should be auto-generated
            if(key=="")
            {
                key = GetUnusedKey();
            }
            ModelReference.Add(new RobotMapEntry { Key = key, Value = value });
            OnPropertyChanged(key);
            UpdateKeyValueList();
        }
        public void RemoveEntry(string key)
        {
            for (int i = 0; i < ModelReference.Count; ++i)
            {
                if (ModelReference[i].Key == key)
                {
                    ModelReference.RemoveAt(i);
                    break;
                }
            }
            OnPropertyChanged(key);
        }
        #endregion

        #region IProvider
        public override string Name
        {
            get
            {
                return "RobotMap";
            }

            set
            {
                throw new InvalidOperationException("Cannot Set Name of Robot Map");
            }
        }
        #endregion

        #region ViewModelBase
        protected override string GetValue(string key)
        {
            foreach(var item in ModelReference)
            {
                if (item.Key == key)
                    return item.Value;
            }
            throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
        }
        protected override void SetValue(string key, string value)
        {
            //remove the entry if value is null
            if (value == null)
                RemoveEntry(key);

            for(int i = 0; i < ModelReference.Count; ++i)
            {
                if (ModelReference[i].Key == key)
                {
                    ModelReference[i].Value = value;
                    return;
                }
            }
            //this view-model should automatically add a new entry if none exists; this is so it functions much like a dictionary
            //throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            AddEntry(key, value);
        }
        protected override List<string> GetOverrideKeys()
        {
            //be careful here; this may be called before construction completes/model instance set
            if (null == ModelReference)
                return new List<string>();

            List<string> ret = new List<string>();
            foreach(var item in ModelReference)
            {
                ret.Add(item.Key);
            }
            return ret;
        }
        protected override void OnModelChange()
        {
            UpdateKeyValueList();
        }
        #endregion

        #region Private Methods
        /*private void OnAddNewEntry(object arg1, AddRobotMapEntryEvent arg2)
        {
            //update the key value list every time a new value is added to keep the datagrid up to date
            UpdateKeyValueList();
        }*/
        #endregion

        #region Private Members
        /// <summary>
        /// Used internally for adding blank entries; checks if <paramref name="key"/> exists in the map
        /// </summary>
        /// <param name="key">the key to check</param>
        /// <returns>whether <paramref name="key"/> exists in the map</returns>
        private bool ContainsKey(string key)
        {
            //loop through each item in the map
            foreach(var item in ModelReference)
            {
                //then compare the keys
                if (item.Key == key)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Used internally for adding new blank entries
        /// </summary>
        /// <returns>a key guaranteed not to be in the map already</returns>
        private string GetUnusedKey()
        {
            //start with a base name
            string baseName = "NewKey";
            string name = baseName;
            int i = 0;

            //until we find an unused name
            while(ContainsKey(name))
            {
                //create the name-number composite and increment the number
                name = baseName + i;
                i++;

                //this is mostly to make sure this while loop does not go on forever
                if (i > 100)
                    throw new Exception("Too Many Auto-Generated RobotMap Entries");
            }
            return name;
        }
        #endregion
    }
}
