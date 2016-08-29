using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class RelayOutputViewModel : ViewModelBase<RelayOutput>, IRelayOutputProvider
    {
        public RelayOutputViewModel(IProvider parent) : base(parent)
        {
        }

        #region IRelayOutputProvider
        public RelayDirection Direction
        {
            get
            {
                return ModelReference.Direction;
            }

            set
            {
                var temp = ModelReference.Direction;
                SetProperty(ref temp, value);
                ModelReference.Direction = temp;
            }
        }

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

        public RelayValue Value
        {
            get
            {
                return ModelReference.Value;
            }

            set
            {
                var ioService = IOService.Instance.RelayOutputs[ID];
                switch (value)
                {
                    case RelayValue.Forward:
                        if (Direction == RelayDirection.ForwardOnly || Direction == RelayDirection.Both)
                        {
                            ioService.ValueAsBool = true;
                            ioService.ReverseValueAsBool = false;
                        }
                        break;
                    case RelayValue.Reverse:
                        if (Direction == RelayDirection.ReverseOnly || Direction == RelayDirection.Both)
                        {
                            ioService.ValueAsBool = false;
                            ioService.ReverseValueAsBool = true;
                        }
                        break;
                    case RelayValue.Off:
                        ioService.ValueAsBool = false;
                        ioService.ReverseValueAsBool = false;
                        break;
                    case RelayValue.On:
                        if (Direction == RelayDirection.ForwardOnly)
                        {
                            ioService.ValueAsBool = true;
                            ioService.ReverseValueAsBool = false;
                        }
                        else if (Direction == RelayDirection.ReverseOnly)
                        {
                            ioService.ValueAsBool = false;
                            ioService.ReverseValueAsBool = true;
                        }
                        else
                        {
                            ioService.ValueAsBool = true;
                            ioService.ReverseValueAsBool = true;
                        }
                        break;
                }
                var temp = ModelReference.Value;
                SetProperty(ref temp, value);
                ModelReference.Value = temp;
            }
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
            switch(key)
            {
                case "Direction":
                    return Direction.ToString();
                case "ID":
                    return ID.ToString();
                case "Name":
                    return Name;
                case "Value":
                    return Value.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "Direction":
                    Direction = SafeCastEnum<RelayDirection>(value);
                        break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                case "Name":
                    Name = value;
                    break;
                case "Value":
                    Value = SafeCastEnum<RelayValue>(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }
        #endregion
    }
}