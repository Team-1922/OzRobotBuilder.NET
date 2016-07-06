using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The information needed for a simple joystick
    /// </summary>
    public class OzJoystickData : INamedClass
    {
        /// <summary>
        /// The name of this joystick
        /// </summary>
        public string Name = "OzJoystickData";
        /// <summary>
        /// The id of this joystick on the driver station
        /// </summary>
        public int JoystickId;
        #region INamedClass Interface
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
