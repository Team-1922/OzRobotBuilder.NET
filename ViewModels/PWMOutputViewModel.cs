using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    public class PWMOutputViewModel : ViewModelBase, IPWMOutputProvider
    {
        protected PWMOutput _pwmOutputModel;

        public void SetPWMOutput(PWMOutput pwmOutput)
        {
            _pwmOutputModel = pwmOutput;
        }

        public int ID
        {
            get
            {
                return _pwmOutputModel.ID;
            }

            set
            {
                var temp = _pwmOutputModel.ID;
                SetProperty(ref temp, value);
                _pwmOutputModel.ID = temp;
            }
        }
        public string Name
        {
            get
            {
                return _pwmOutputModel.Name;
            }

            set
            {
                var temp = _pwmOutputModel.Name;
                SetProperty(ref temp, value);
                _pwmOutputModel.Name = temp;
            }
        }
        public double Value
        {
            get
            {
                return _pwmOutputModel.Value = IOService.Instance.PWMOutputs[ID].Value;
            }

            set
            {
                var temp = _pwmOutputModel.Value;
                SetProperty(ref temp, IOService.Instance.PWMOutputs[ID].Value = TypeRestrictions.Clamp("PWMOutput.Value", value));
                _pwmOutputModel.Value = temp;
            }
        }
    }
}