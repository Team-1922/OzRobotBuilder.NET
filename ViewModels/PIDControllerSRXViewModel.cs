using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSRXViewModel : ViewModelBase<PIDControllerSRX>, IPIDControllerSRXProvider
    {
        PIDControllerSRX _pidControllerSRXModel;

        public PIDControllerSRXViewModel(ICANTalonProvider parent) : base(parent)
        {
        }

        #region IPIDControllerSRXProvider
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

        public void SetPIDController(PIDControllerSRX pidController)
        {
            _pidControllerSRXModel = pidController;
        }
        #endregion

        #region IProvider
        public string Name
        {
            get
            {
                return "PID Controller";
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
                case "AllowableCloseLoopError":
                    return AllowableCloseLoopError.ToString();
                case "CloseLoopRampRate":
                    return CloseLoopRampRate.ToString();
                case "P":
                    return P.ToString();
                case "I":
                    return I.ToString();
                case "D":
                    return D.ToString();
                case "F":
                    return F.ToString();
                case "IZone":
                    return IZone.ToString();
                case "SourceType":
                    return SourceType.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }

        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "AllowableCloseLoopError":
                    AllowableCloseLoopError = SafeCastInt(value);
                    break;
                case "CloseLoopRampRate":
                    CloseLoopRampRate = SafeCastDouble(value);
                    break;
                case "P":
                    P = SafeCastDouble(value);
                    break;
                case "I":
                    I = SafeCastDouble(value);
                    break;
                case "D":
                    D = SafeCastDouble(value);
                    break;
                case "F":
                    F = SafeCastDouble(value);
                    break;
                case "IZone":
                    IZone = SafeCastInt(value);
                    break;
                case "SourceType":
                    SourceType = SafeCastEnum<CANTalonDifferentiationLevel>(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _pidControllerSRXModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        protected override PIDControllerSRX ModelInstance
        {
            get
            {
                return _pidControllerSRXModel;
            }

            set
            {
                SetPIDController(value);
            }
        }
        #endregion
    }
}