using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{

    class CompoundProviderList<ProviderType, ModelType> : ViewModelBase<List<ModelType>>, IEnumerable<ProviderType>, ICompoundProvider where ProviderType : IProvider<ModelType>
    {
        //NOTE: if items here are manually added/removed, make SURE they are added to the model
        public IObservableCollection<ProviderType> Items
        {
            get
            {
                return _items;
            }
        }

        public CompoundProviderList(string name, IProvider parent, Func<ModelType, ProviderType> constructNewItem) : base(parent)
        {
            Name = name;
            _items.CollectionChanged += Items_CollectionChanged;
            _constructNewItem = constructNewItem;
        }


        public IReadOnlyDictionary<string, string> Properties
        {
            get
            {
                return (from child in Children select child).ToDictionary(child => child.Name, child => (child.ToString() ?? "null"));
            }
        }

        public void AddOrUpdate(ModelType item)
        {
            //TODO: is there a way to do this without constructing the entire ProviderType first?
            var newProvider = _constructNewItem(item);
            bool exists = false;
            for(int i = 0; i < _items.Count; ++i)
            {
                if(newProvider.Name == _items[i].Name)
                {
                    _items[i] = newProvider;
                    exists = true;
                    break;
                }
            }
            if(!exists)
            {
                _items.Add(newProvider);
            }
            RegisterChildEventPropagation(newProvider);
        }
        public void Remove(string name)
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                if (_items[i].Name == name)
                {
                    //remove the provider
                    _items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.RemoveAt(i);
                    break;
                }
            }
            throw new ArgumentException($"Could Not Find {name}", "name");
        }

        /// <summary>
        /// Used internally for adding blank entries; checks if <paramref name="name"/> exists in the map
        /// </summary>
        /// <param name="name">the key to check</param>
        /// <returns>whether <paramref name="name"/> exists in the map</returns>
        private bool ContainsName(string name)
        {
            //loop through each item in the map
            foreach (var item in _items)
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
            return (from item in _items select item.Name).ToList();
        }
        protected override string GetValue(string key)
        {
            return FindByName(key).GetModelJson();
        }
        protected override void SetValue(string key, string value)
        {
            //add a new item if "key" is blank
            if(key == "")
            {
            }
            
            //delete the item if "value" is null
            if(value == null)
            {
                Remove(key);
                return;
            }

            if (Contains(key))
            {
                FindByName(key).SetModelJson(value);
            }
            else
            {
                AddNew(value);
            }
        }
        protected override void OnModelChange()
        {
            //_items.Clear();
            foreach(var item in ModelReference)
            {
                AddOrUpdate(item);
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
        public IObservableCollection<ProviderType> Children
        {
            get
            {
                return _items;
            }
        }
        #endregion

        #region IProvider
        public override string Name
        {
            get;
            set;
        }
        #endregion

        #region IEnumerable<ProviderType>
        public IEnumerator<ProviderType> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Private Methods
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateKeyValueList();
            //RefreshTypedItemsList();
        }
        private IProvider FindByName(string name)
        {
            foreach (var item in _items)
            {
                if (item.Name == name)
                    return item;
            }
            throw new ArgumentException($"\"{name}\" Does Not Exist");
        }
        private bool Contains(string name)
        {
            foreach(var item in _items)
            {
                if (item.Name == name)
                    return true;
            }
            return false;
        }
        private void AddNew(string value)
        {
            var newItem = JsonDeserialize<ModelType>(value);
            if (newItem != null)
            {
                ModelReference.Add(newItem);
                AddOrUpdate(newItem);
            }
        }
        #endregion

        #region Private Fields
        Func<ModelType, ProviderType> _constructNewItem;
        ObservableCollection<ProviderType> _items = new ObservableCollection<ProviderType>();
        #endregion
    }
}
