using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class PWMOutputViewModel : ViewModelBase<PWMOutput>, IPWMOutputProvider
    {
        public PWMOutputViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        #region IPWMOutputProvider
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
        public double Value
        {
            get
            {
                return ModelReference.Value;
            }

            set
            {
                var temp = ModelReference.Value;
                SetProperty(ref temp, IOService.Instance.PWMOutputs[ID].Value = TypeRestrictions.Clamp("PWMOutput.Value", value));
                ModelReference.Value = temp;
            }
        }
        #endregion

        #region IProvider
        public string Name
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
            switch(key)
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
                case "Value":
                    Value = SafeCastDouble(value);
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }

        }        
        #endregion
    }
}