using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The base trigger data which contains information about the command which is called
    /// </summary>
    public class OzTriggerDataBase : INamedClass
    {
        /// <summary>
        /// the name of this trigger
        /// </summary>
        public string Name = "OzTriggerData";
        /// <summary>
        /// the command to run when the trigger condition is met
        /// </summary>
        public string CommandName;
        /// <summary>
        /// the parameters to the command name
        /// </summary>
        public List<string> CommandParameters;
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
    /// <summary>
    /// A trigger which is called when a particular joystick button is pressed
    /// </summary>
    public class OzJoystickTriggerData : OzTriggerDataBase
    {
        /// <summary>
        /// Under what button state is this command run?
        /// </summary>
        public enum OzTriggerType
        {
            /// <summary>
            /// when the button is first pressed
            /// </summary>
            WhenPressed,
            /// <summary>
            /// after the butten is released
            /// </summary>
            WhenReleased,
            /// <summary>
            /// the whole duration the button is held
            /// </summary>
            WhileHeld
        }
        /// <summary>
        /// The name of the joystick this is mapped to
        /// </summary>
        public string JoystickName;
        /// <summary>
        /// the button on the joystick this is mapped to
        /// </summary>
        public int JoystickButton;
        /// <summary>
        /// The condition this trigger is called under
        /// </summary>
        public OzTriggerType TriggerType;
    }
    /// <summary>
    /// This class represents a command that does not explicitly require any particular 
    ///     joystick button (i.e. operator controlled drive train)
    /// </summary>
    public class OzContinuousTriggerData : OzTriggerDataBase
    {
    }
}
