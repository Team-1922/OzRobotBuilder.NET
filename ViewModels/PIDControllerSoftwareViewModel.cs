using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSoftwareViewModel : IPIDControllerSoftwareProvider
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
                _pidControllerSoftwareModel.Continuous = value;
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
                _pidControllerSoftwareModel.CycleDuration = value;
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
                _pidControllerSoftwareModel.P = value;
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
                _pidControllerSoftwareModel.I = value;
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
                _pidControllerSoftwareModel.D = value;
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
                _pidControllerSoftwareModel.F = value;
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
                _pidControllerSoftwareModel.Tolerance = value;
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