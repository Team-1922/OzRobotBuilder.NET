using CommonLib.Validation;
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
    public class OzTriggerDataBase : INamedClass, IValidatable
    {
        /// <summary>
        /// the name of this trigger
        /// </summary>
        public string Name { get; set; } = "OzTriggerData";
        /// <summary>
        /// the command to run when the trigger condition is met
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// the parameters to the command name
        /// </summary>
        public List<string> CommandParameters { get; set; } = new List<string>();
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, Name);

            if(settings.Contains(ValidationSetting.NullValues))
            {
                if (null == CommandParameters)
                    ret.ValidationIssues.Add(new NullValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "CommandParameters")));
            }

            if (settings.Contains(ValidationSetting.IllogicalValues))
            {
                if (!ValidationUtils.CheckName(Name))
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "Name"), Name));
            }

            return ret;
        }
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
        public string JoystickName { get; set; }
        /// <summary>
        /// the button on the joystick this is mapped to
        /// </summary>
        public int JoystickButton { get; set; }
        /// <summary>
        /// The condition this trigger is called under
        /// </summary>
        public OzTriggerType TriggerType { get; set; }
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public new ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = base.Validate(settings, workingPath);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, Name);

            if (settings.Contains(ValidationSetting.IllogicalValues))
            {
                //joystick button is not limited, becuase that depends on which joystick is being used
            }

            return ret;
        }
    }
    /// <summary>
    /// This class represents a command that does not explicitly require any particular 
    ///     joystick button (i.e. operator controlled drive train)
    /// </summary>
    public class OzContinuousTriggerData : OzTriggerDataBase
    {
    }
}
