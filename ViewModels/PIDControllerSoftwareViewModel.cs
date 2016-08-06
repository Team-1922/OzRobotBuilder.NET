using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSoftwareViewModel : ViewModelBase, IPIDControllerSoftwareProvider
    {
        PIDControllerSoftware _pidControllerSoftwareModel;

        public bool Continuous
        {
            get
            {
                return _pidControllerSoftwareModel.Continuous;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.Continuous;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.Continuous = temp;
            }
        }

        public double CycleDuration
        {
            get
            {
                return _pidControllerSoftwareModel.CycleDuration;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.CycleDuration;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.CycleDuration = temp;
            }
        }

        public double P
        {
            get
            {
                return _pidControllerSoftwareModel.P;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.P;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.P = temp;
            }
        }

        public double I
        {
            get
            {
                return _pidControllerSoftwareModel.I;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.I;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.I = temp;
            }
        }

        public double D
        {
            get
            {
                return _pidControllerSoftwareModel.D;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.D;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.D = temp;
            }
        }

        public double F
        {
            get
            {
                return _pidControllerSoftwareModel.F;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.F;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.F = temp;
            }
        }

        public double Tolerance
        {
            get
            {
                return _pidControllerSoftwareModel.Tolerance;
            }

            set
            {
                var temp = _pidControllerSoftwareModel.Tolerance;
                SetProperty(ref temp, value);
                _pidControllerSoftwareModel.Tolerance = temp;
            }
        }

        public string Name
        {
            get
            {
                return "PID Controller";
            }
        }

        public void SetPIDController(PIDControllerSoftware pidController)
        {
            _pidControllerSoftwareModel = pidController;
        }
    }
}