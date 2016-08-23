using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{

    class CompoundProviderList<ProviderType, ModelType> : ViewModelBase<List<ModelType>>, IEnumerable<ProviderType>, ICompoundProvider where ProviderType : IProvider<ModelType>
    {
        //NOTE: if items here are manually added/removed, make SURE they are added to the model
        public ObservableCollection<ProviderType> Items { get; } = new ObservableCollection<ProviderType>();

        public CompoundProviderList(string name, IProvider parent, Func<ModelType, ProviderType> constructNewItem) : base(parent)
        {
            Name = name;
            Items.CollectionChanged += Items_CollectionChanged;
            _constructNewItem = constructNewItem;
        }


        public IReadOnlyDictionary<string, string> Properties
        {
            get
            {
                return (from child in Children select child).ToDictionary(child => child.Name, child => (child.ToString() ?? "null"));
            }
        }

        /// <summary>
        /// Used internally for adding blank entries; checks if <paramref name="name"/> exists in the map
        /// </summary>
        /// <param name="name">the key to check</param>
        /// <returns>whether <paramref name="name"/> exists in the map</returns>
        private bool ContainsName(string name)
        {
            //loop through each item in the map
            foreach (var item in Items)
            {
                //then compare the keys
                if (item.Name == name)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Used for adding entries when the incoming name may be the same as another instance in the list
        /// </summary>
        /// <param name="baseName">the name to test against</param>
        /// <returns>a key guaranteed not to be in the map already</returns>
        public string GetUnusedKey(string baseName)
        {
            //start with a base name
            string name = baseName;
            int i = 0;

            //until we find an unused name
            while (ContainsName(name))
            {
                //create the name-number composite and increment the number
                name = baseName + i;
                i++;

                //this is mostly to make sure this while loop does not go on forever
                if (i > 100)
                    throw new Exception("Too Many Similarly Named Entries");
            }
            return name;
        }

        #region ViewModelBase
        protected override List<string> GetOverrideKeys()
        {
            return (from item in Items select item.Name).ToList();
        }
        protected override string GetValue(string key)
        {
            var item = FindByName(key);
            if(null != item)
            {
                return item.GetModelJson();
            }
            throw new ArgumentException("item not found", "key");
        }
        protected override void SetValue(string key, string value)
        {
            //add a new item if "key" is blank
            if(key == "")
            {
                var newItem = JsonDeserialize<ModelType>(value);
                if(newItem != null)
                    ModelReference.Add(newItem);
            }

            var item = FindByName(key);
            if(null != item)
            {
                item.SetModelJson(value);
                return;
            }
            throw new ArgumentException($"\"{key}\" is Read-Only");
        }
        protected override void OnModelChange()
        {
            Items.Clear();
            foreach(var item in ModelReference)
            {
                Items.Add(_constructNewItem(item));
            }
        }
        #endregion

        #region ICompoundProvider
        public IEnumerable<IProvider> Children
        {
            get
            {
                return Items.Cast<IProvider>();
            }
        }
        #endregion

        #region IProvider
        public string Name
        {
            get;
        }
        #endregion

        #region IEnumerable<ProviderType>
        public IEnumerator<ProviderType> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion

        #region Private Methods
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateKeyValueList();
        }

        private ProviderType FindByName(string name)
        {
            foreach (var item in Items)
            {
                if (item.Name == name)
                    return item;
            }
            throw new ArgumentException($"\"{name}\" Does Not Exist");
        }
        #endregion

        #region Private Fields
        Func<ModelType, ProviderType> _constructNewItem;
        #endregion
    }
}
