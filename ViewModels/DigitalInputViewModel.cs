using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class DigitalInputViewModel : ViewModelBase<DigitalInput>, IDigitalInputProvider
    {
        public DigitalInputViewModel(IProvider parent) : base(parent)
        {
        }

        #region IDigitalInputProvider
        public int ID
        {
            get
            {
                return ModelReference.ID;
            }

            set
            {
                var temp = ModelReference.ID;
                SetProperty(ref temp, value);
                ModelReference.ID = temp;
            }
        }
        public bool Value
        {
            get
            {
                return ModelReference.Value;
            }

            private set
            {
                var temp = ModelReference.Value;
                SetProperty(ref temp, value);
                ModelReference.Value = temp;
            }
        }
        #endregion

        #region IInputProvider
        public void UpdateInputValues()
        {
            Value = IOService.Instance.DigitalInputs[ID].ValueAsBool;
        }
        #endregion

        #region IProvider
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
        #endregion

        #region ViewModelBase
        protected override string GetValue(string key)
        {
            switch (key)
            {
                case "Name":
                    return Name;
                case "Value":
                    return Value.ToString();
                case "ID":
                    return ID.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "Name":
                    Name = value;
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }
        #endregion
    }
}
