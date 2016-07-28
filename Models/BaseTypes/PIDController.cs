using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.Deprecated.BaseTypes
{
    /// <summary>
    /// PID Configuration for software PID Controller (use <see cref="CANTalon.CANTalonPIDController"/>  for SRX PID configuration)
    /// </summary>
    public class PIDController : BindableBase
    {
        /// <summary>
        /// The Proportional Constant
        /// </summary>
        public double P
        {
            get { return _p; }
            set { SetProperty(ref _p, value); }
        }
        /// <summary>
        /// The Integration Constant
        /// </summary>
        public double I
        {
            get { return _i; }
            set { SetProperty(ref _i, value); }
        }
        /// <summary>
        /// The Differential Constant
        /// </summary>
        public double D
        {
            get { return _d; }
            set { SetProperty(ref _d, value); }
        }
        /// <summary>
        /// The Feed-Forward Constant
        /// </summary>
        public double F
        {
            get { return _f; }
            set { SetProperty(ref _f, value); }
        }
        /// <summary>
        /// The allowable tolerance in the PID input (replaced with <see cref="CANTalon.CANTalonPIDController.AllowableCloseLoopError"/> on Talon SRXs)
        /// </summary>
        public double Tolerance
        {
            get { return _tolerance; }
            set { SetProperty(ref _tolerance, value); }
        }
        /// <summary>
        /// The time in seconds between update cycles (this is irrelevent on the SRX)
        /// </summary>
        public double CycleDuration
        {
            get { return _cycleDuration; }
            set { SetProperty(ref _cycleDuration, value); }
        }
        /// <summary>
        /// Whether or not the input is like a continuous rotation potentiometer (usually no)
        /// </summary>
        public bool Continuous
        {
            get { return _continuous; }
            set { SetProperty(ref _continuous, value); }
        }

        #region Private Fields
        private double _p;
        private double _i;
        private double _d;
        private double _f;
        private double _tolerance;
        private double _cycleDuration;
        private bool _continuous;
        #endregion
    }
}
