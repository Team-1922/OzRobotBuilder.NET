using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSRXViewModel : ViewModelBase, IPIDControllerSRXProvider
    {
        PIDControllerSRX _pidControllerSRXModel;

        public int AllowableCloseLoopError
        {
            get
            {
                return _pidControllerSRXModel.AllowableCloseLoopError;
            }

            set
            {
                var temp = _pidControllerSRXModel.AllowableCloseLoopError;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.AllowableCloseLoopError = temp;
            }
        }

        public double CloseLoopRampRate
        {
            get
            {
                return _pidControllerSRXModel.CloseLoopRampRate;
            }

            set
            {
                var temp = _pidControllerSRXModel.CloseLoopRampRate;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.CloseLoopRampRate = temp;
            }
        }

        public double P
        {
            get
            {
                return _pidControllerSRXModel.P;
            }

            set
            {
                var temp = _pidControllerSRXModel.P;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.P = temp;
            }
        }

        public double I
        {
            get
            {
                return _pidControllerSRXModel.I;
            }

            set
            {
                var temp = _pidControllerSRXModel.I;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.I = temp;
            }
        }

        public double D
        {
            get
            {
                return _pidControllerSRXModel.D;
            }

            set
            {
                var temp = _pidControllerSRXModel.D;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.D = temp;
            }
        }

        public double F
        {
            get
            {
                return _pidControllerSRXModel.F;
            }

            set
            {
                var temp = _pidControllerSRXModel.F;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.F = temp;
            }
        }

        public int IZone
        {
            get
            {
                return _pidControllerSRXModel.IZone;
            }

            set
            {
                var temp = _pidControllerSRXModel.IZone;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.IZone = temp;
            }
        }

        public CANTalonDifferentiationLevel SourceType
        {
            get
            {
                return _pidControllerSRXModel.SourceType;
            }

            set
            {
                var temp = _pidControllerSRXModel.SourceType;
                SetProperty(ref temp, value);
                _pidControllerSRXModel.SourceType = temp;
            }
        }

        public string Name
        {
            get
            {
                return "PID Controller";
            }
        }

        public void SetPIDController(PIDControllerSRX pidController)
        {
            _pidControllerSRXModel = pidController;
        }
    }
}