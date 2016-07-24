using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models
{
    /// <summary>
    /// The information needed for a simple joystick
    /// </summary>
    public class Joystick : BindableBase, INamedClass, IIDNumber
    {
        /// <summary>
        /// The name of this joystick
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The id of this joystick on the driver station
        /// </summary>
        public uint ID
        {
            get { return _iD; }
            set { SetProperty(ref _iD, value); }
        }
        /// <summary>
        /// Contains the states of all of the buttons on the joystick; not used in the editor, but used for triggers on the robot
        /// </summary>
        public ObservableDictionary<uint, bool> ButtonStates { get; set; } = new ObservableDictionary<uint, bool>();

        #region Private Fields
        private string _name = "OzJoystickData";
        private uint _iD;
        #endregion
    }
}
