using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class CommandGroupViewModel : ViewModelBase<CommandGroup>, ICommandGroupProvider
    {
        public CommandGroupViewModel(IProvider parent) : base(parent)
        {
        }

        #region ICommandGroupProvider
        public IObservableCollection<ICommandGroupItemProvider> Commands
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void InsertCommandGroupItem(CommandGroupItem item, int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveCommandGroupItem(int index)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ViewModelBase
        public override string Name
        {
            get
            {
                return ModelReference.Name;
            }

            set
            {
                var temp = ModelReference.Name;
                SetProperty(ref temp, value);
                ModelReference.Name = temp;
            }
        }

        protected override string GetValue(string key)
        {
            throw new NotImplementedException();
        }

        protected override void SetValue(string key, string value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
