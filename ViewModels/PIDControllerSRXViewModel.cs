using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    //This is broken in a few ways; namely not being bound to the IO service at all
    internal class PIDControllerSRXViewModel : ViewModelBase<PIDControllerSRX>, IPIDControllerSRXProvider
    {
        /// <summary>
        /// Construct a new PIDControllerSRXViewModel
        /// </summary>
        /// <param name="parent">The ICANTalonProvider Associated with this PIDController</param>
        /// <param name="pidConfig">whether this is the 0th PID Controller configuration or the 1st</param>
        public PIDControllerSRXViewModel(ICANTalonProvider parent, bool pidConfig) : base(parent)
        {
            _parent = parent;
            _pidConfig = pidConfig;
        }

        #region IPIDControllerSRXProvider
        public int AllowableCloseLoopError
        {
            get
            {
                return ModelReference.AllowableCloseLoopError;
            }

            set
            {
                var temp = ModelReference.AllowableCloseLoopError;
                SetProperty(ref temp, 
                    _pidConfig ? 
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.AllowableCloseLoopError = value : 
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.AllowableCloseLoopError = value);
                ModelReference.AllowableCloseLoopError = temp;
            }
        }

        public double CloseLoopRampRate
        {
            get
            {
                return ModelReference.CloseLoopRampRate;
            }

            set
            {
                var temp = ModelReference.CloseLoopRampRate;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.CloseLoopRampRate = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.CloseLoopRampRate = value);
                ModelReference.CloseLoopRampRate = temp;
            }
        }

        public double P
        {
            get
            {
                return ModelReference.P;
            }

            set
            {
                var temp = ModelReference.P;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.P = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.P = value);
                ModelReference.P = temp;
            }
        }

        public double I
        {
            get
            {
                return ModelReference.I;
            }

            set
            {
                var temp = ModelReference.I;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.I = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.I = value);
                ModelReference.I = temp;
            }
        }

        public double D
        {
            get
            {
                return ModelReference.D;
            }

            set
            {
                var temp = ModelReference.D;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.D = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.D = value);
                ModelReference.D = temp;
            }
        }

        public double F
        {
            get
            {
                return ModelReference.F;
            }

            set
            {
                var temp = ModelReference.F;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.F = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.F = value);
                ModelReference.F = temp;
            }
        }

        public int IZone
        {
            get
            {
                return ModelReference.IZone;
            }

            set
            {
                var temp = ModelReference.IZone;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.IZone = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.IZone = value);
                ModelReference.IZone = temp;
            }
        }

        public CANTalonDifferentiationLevel SourceType
        {
            get
            {
                return ModelReference.SourceType;
            }

            set
            {
                var temp = ModelReference.SourceType;
                SetProperty(ref temp,
                    _pidConfig ?
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig0.SourceType = value :
                    IOService.Instance.CANTalons[_parent.ID].PIDConfig1.SourceType = value);
                ModelReference.SourceType = temp;
            }
        }
        #endregion

        #region IProvider
        public override string Name
        {
            get
            {
                return "PID Controller";
            }

            set
            {
                throw new InvalidOperationException("Cannot Set Name of CAN Talon PID Controller");
            }
        }
        #endregion

        #region Private Fields
        private ICANTalonProvider _parent;
        private bool _pidConfig;
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
        #endregion
    }
}