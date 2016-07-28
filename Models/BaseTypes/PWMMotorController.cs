using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Models.Deprecated.BaseTypes
{
    /// <summary>
    /// Data for a simple PWM motor controller
    /// </summary>
    public class PWMMotorController : BindableBase, INamedClass, IIDNumber
    {
        /// <summary>
        /// The name of this motor controller
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The pwm ID of the motor on the RoboRIO
        /// </summary>
        public uint ID
        {
            get { return _iD; }
            set { SetProperty(ref _iD, value); }
        }

        #region Private Fields
        private string _name = "OzMotorControllerData";
        private uint _iD;
        #endregion
    }
}
