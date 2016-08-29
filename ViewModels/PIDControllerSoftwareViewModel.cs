using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class PIDControllerSoftwareViewModel : ViewModelBase<PIDControllerSoftware>, IPIDControllerSoftwareProvider
    {
        public PIDControllerSoftwareViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        #region IPIDControllerSoftwareProvider
        public bool Continuous
        {
            get
            {
                return ModelReference.Continuous;
            }

            set
            {
                var temp = ModelReference.Continuous;
                SetProperty(ref temp, value);
                ModelReference.Continuous = temp;
            }
        }

        public double CycleDuration
        {
            get
            {
                return ModelReference.CycleDuration;
            }

            set
            {
                var temp = ModelReference.CycleDuration;
                SetProperty(ref temp, value);
                ModelReference.CycleDuration = temp;
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
                SetProperty(ref temp, value);
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
                SetProperty(ref temp, value);
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
                SetProperty(ref temp, value);
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
                SetProperty(ref temp, value);
                ModelReference.F = temp;
            }
        }

        public double Tolerance
        {
            get
            {
                return ModelReference.Tolerance;
            }

            set
            {
                var temp = ModelReference.Tolerance;
                SetProperty(ref temp, value);
                ModelReference.Tolerance = temp;
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
                throw new InvalidOperationException("Cannot Set Name of Software PID Controller");
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
                    case "Continuous":
                        return Continuous.ToString();
                    case "CycleDuration":
                        return CycleDuration.ToString();
                    case "P":
                        return P.ToString();
                    case "I":
                        return I.ToString();
                    case "D":
                        return D.ToString();
                    case "F":
                        return F.ToString();
                    case "Tolerance":
                        return Tolerance.ToString();
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "Continuous":
                    Continuous = SafeCastBool(value);
                    break;
                case "CycleDuration":
                    CycleDuration = SafeCastDouble(value);
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
                case "Tolerance":
                    Tolerance = SafeCastDouble(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }
        #endregion
    }
}