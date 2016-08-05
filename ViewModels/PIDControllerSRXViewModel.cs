using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSRXViewModel : IPIDControllerSRXProvider
    {
        PIDControllerSRX _pidControllerSRXVModel;

        public int AllowableCloseLoopError
        {
            get
            {
                return _pidControllerSRXVModel.AllowableCloseLoopError;
            }

            set
            {
                _pidControllerSRXVModel.AllowableCloseLoopError = value;
            }
        }

        public double CloseLoopRampRate
        {
            get
            {
                return _pidControllerSRXVModel.CloseLoopRampRate;
            }

            set
            {
                _pidControllerSRXVModel.CloseLoopRampRate = value;
            }
        }

        public double P
        {
            get
            {
                return _pidControllerSRXVModel.P;
            }

            set
            {
                _pidControllerSRXVModel.P = value;
            }
        }

        public double I
        {
            get
            {
                return _pidControllerSRXVModel.I;
            }

            set
            {
                _pidControllerSRXVModel.I = value;
            }
        }

        public double D
        {
            get
            {
                return _pidControllerSRXVModel.D;
            }

            set
            {
                _pidControllerSRXVModel.D = value;
            }
        }

        public double F
        {
            get
            {
                return _pidControllerSRXVModel.F;
            }

            set
            {
                _pidControllerSRXVModel.F = value;
            }
        }

        public int IZone
        {
            get
            {
                return _pidControllerSRXVModel.IZone;
            }

            set
            {
                _pidControllerSRXVModel.IZone = value;
            }
        }

        public CANTalonDifferentiationLevel SourceType
        {
            get
            {
                return _pidControllerSRXVModel.SourceType;
            }

            set
            {
                _pidControllerSRXVModel.SourceType = value;
            }
        }

        public void SetPIDController(PIDControllerSRX pidController)
        {
            _pidControllerSRXVModel = pidController;
        }
    }
}