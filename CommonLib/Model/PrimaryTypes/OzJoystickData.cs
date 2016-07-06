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
        public string Name { get; set; } = "OzJoystickData";
        /// <summary>
        /// The id of this joystick on the driver station
        /// </summary>
        public int JoystickId { get; set; }
    }
}
