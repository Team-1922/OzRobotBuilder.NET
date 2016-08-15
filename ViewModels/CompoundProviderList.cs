using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{

    class CompoundProviderList<T> : ViewModelBase, ICompoundProvider where T : IProvider
    {
        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

        public CompoundProviderList(string name)
        {
            Name = name;
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateKeyValueList();
        }

        protected override List<string> GetOverrideKeys()
        {
            return (from item in Items select item.Name).ToList();
        }

        private T FindByName(string name)
        {
            foreach(var item in Items)
            {
                if (item.Name == name)
                    return item;
            }
            throw new Exception();
        }

        protected override string GetValue(string key)
        {
            try
            {
                return FindByName(key).ToString();
            }
            catch(Exception)
            {
                throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }

        protected override void SetValue(string key, string value)
        {
            throw new ArgumentException($"\"{key}\" is Read-Only");
        }

        public IEnumerable<IProvider> Children
        {
            get
            {
                return Items.Cast<IProvider>();
            }
        }

        public string Name
        {
            get;
        }

        public IReadOnlyDictionary<string, string> Properties
        {
            get
            {
                return (from child in Children select child).ToDictionary(child => child.Name, child => (child.ToString() ?? "null"));
            }
        }
    }
}
