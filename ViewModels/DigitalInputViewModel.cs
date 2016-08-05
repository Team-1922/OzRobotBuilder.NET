using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class DigitalInputViewModel : IDigitalInputProvider
    {
        DigitalInput _digitalInputModel;

        public int ID
        {
            get
            {
                return _digitalInputModel.ID;
            }

            set
            {
                _digitalInputModel.ID = (int)TypeRestrictions.Clamp("DigitalInput.ID", value);
            }
        }

        public string Name
        {
            get
            {
                return _digitalInputModel.Name;
            }

            set
            {
                _digitalInputModel.Name = value;
            }
        }

        public bool Value
        {
            get
            {
                return _digitalInputModel.Value;
            }
        }

        public void SetDigitalInput(DigitalInput digitalInput)
        {
            _digitalInputModel = digitalInput;
        }

        public void UpdateInputValues()
        {
            _digitalInputModel.Value = IOService.Instance.DigitalInputs[ID].ValueAsBool;
        }
    }
}
