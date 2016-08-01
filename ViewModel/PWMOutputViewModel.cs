using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    public class PWMOutputViewModel : IPWMOutputProvider
    {
        protected PWMOutput _pwmOutputModel;

        public void SetPWMOutput(PWMOutput pwmOutput)
        {
            throw new NotImplementedException();
        }

        public int ID
        {
            get { return _pwmOutputModel.ID; }
            set { _pwmOutputModel.ID = value; }
        }
        public string Name
        {
            get { return _pwmOutputModel.Name; }
            set { _pwmOutputModel.Name = value; }
        }
        public double Value
        {
            get { return _pwmOutputModel.Value = IOService.Instance.PWMOutputs[ID].Value; }
            set { _pwmOutputModel.Value = IOService.Instance.PWMOutputs[ID].Value = TypeRestrictions.Clamp("PWMOutput.Value", value); }
        }
    }
}