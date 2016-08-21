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

        public PIDControllerSoftwareViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        #region IPIDControllerSoftwareProvider
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

        public void SetPIDController(PIDControllerSoftware pidController)
        {
            _pidControllerSoftwareModel = pidController;
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
        public string GetModelJson()
        {
            return JsonSerialize(_pidControllerSoftwareModel);
        }
        public void SetModelJson(string text)
        {
            SetPIDController(JsonDeserialize<PIDControllerSoftware>(text));
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

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _pidControllerSoftwareModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }
        #endregion
    }
}