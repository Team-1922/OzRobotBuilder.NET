using CommonLib.Interfaces;
using CommonLib.Model.CompositeTypes;
using CommonLib.Model.PrimaryTypes;
using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.Documents
{
    /// <summary>
    /// The entire robot application; subsystems, commands, operator interfaces and everything
    /// </summary>
    public class RobotDocument : Document, IValidatable
    {
        /// <summary>
        /// the path of this file in the directory structure
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The name of the robot document; this is always the same becuase only one document can be opened at once
        /// </summary>
        public string Name { get; private set; } = "RobotDocument";
        /// <summary>
        /// A list of all of the subsystems on this robot
        /// </summary>
        public UniqueItemList<OzSubsystemData> Subsystems { get; set; } = new UniqueItemList<OzSubsystemData>();
        /// <summary>
        /// A list of all of the commands on this robot
        /// </summary>
        public UniqueItemList<OzCommandData> Commands { get; set; } = new UniqueItemList<OzCommandData>();
        /// <summary>
        /// A list of all of the joysticks configured for this program
        /// </summary>
        public UniqueItemList<OzJoystickData> Joysticks { get; set; } = new UniqueItemList<OzJoystickData>();
        /// <summary>
        /// A list of all of the triggers configured for this operator control
        /// </summary>
        public UniqueItemList<OzTriggerDataBase> Triggers { get; set; } = new UniqueItemList<OzTriggerDataBase>();
        /// <summary>
        /// Validates Document
        /// </summary>
        /// <param name="settings">the validation settings to use</param>
        /// <param name="workingPath">the path to this object to be validated; automatically overwritten in this class</param>
        /// <returns>a report of validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath = "")
        {
            ValidationReport ret = new ValidationReport(settings);

            workingPath = Name;
            ret.Augment(ValidateSubsystems(settings, workingPath));
            ret.Augment(ValidateCommands(settings, workingPath));
            ret.Augment(ValidateJoysticks(settings, workingPath));
            ret.Augment(ValidateTriggers(settings, workingPath));

            //TODO: UnusedElements

            return ret;
        }

        #region Subobject Validation
        private ValidationReport ValidateSubsystems(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "Subsystems");

            #region Null Validation
            if (null == Subsystems)//container null-check
            {
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
                return ret;
            }
            else
                for (int i = 0; i < Subsystems.Count; ++i)
                    if (null == Subsystems[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.Augment(Subsystems[i].Validate(settings, workingPath));//individual validation
            #endregion

            #region Name Validation
            //group validation (name validation)
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                var nameIssues = ValidationUtils.ReusedNamesValidation(Subsystems, workingPath);
                if (null != nameIssues)
                    ret.ValidationIssues.AddRange(nameIssues);
            }
            #endregion

            #region Reused ID Validation
            if (settings.Contains(ValidationSetting.ReusedIds))
            {
                //a map for each id used
                var usedAnalogInputs = new Dictionary<uint, List<string>>();
                var usedPWMOutputs = new Dictionary<uint, List<string>>();
                var usedSRXOutputs = new Dictionary<uint, List<string>>();
                var usedDigitalInputs = new Dictionary<uint, List<string>>();

                //go through each subsystem once
                foreach (var subsystem in Subsystems)
                {
                    if (subsystem == null)
                        continue;

                    string workingSubsystemPath = ValidationUtils.ExtendWorkingPath(workingPath, subsystem.Name);

                    //analog inputs
                    ValidationUtils.GetNamesOfId(subsystem.AnalogInputs, workingSubsystemPath, ref usedAnalogInputs);

                    //PWM outputs
                    ValidationUtils.GetNamesOfId(subsystem.PWMMotorControllers, workingSubsystemPath, ref usedPWMOutputs);

                    //CAN Ids
                    ValidationUtils.GetNamesOfId(subsystem.TalonSRXs, workingSubsystemPath, ref usedSRXOutputs);

                    //digital inputs
                    ValidationUtils.GetNamesOfId(subsystem.DigitalInputs, workingSubsystemPath, ref usedDigitalInputs);

                    //special instance for quad encoders
                    foreach (var quadEncoder in subsystem.QuadEncoders)
                    {
                        if (quadEncoder == null)
                            continue;
                        string workingEncoderPath = ValidationUtils.ExtendWorkingPath(workingSubsystemPath, quadEncoder.Name);
                        if (usedDigitalInputs.ContainsKey(quadEncoder.ID))
                            usedDigitalInputs[quadEncoder.ID].Add(workingEncoderPath);
                        else
                            usedDigitalInputs.Add(quadEncoder.ID, new List<string>() { workingEncoderPath });

                        if (usedDigitalInputs.ContainsKey(quadEncoder.ID1))
                            usedDigitalInputs[quadEncoder.ID1].Add(workingEncoderPath);
                        else
                            usedDigitalInputs.Add(quadEncoder.ID1, new List<string>() { workingEncoderPath });
                    }

                }

                //now create the issues from any conflicts
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(usedAnalogInputs));
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(usedPWMOutputs));
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(usedSRXOutputs));
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(usedDigitalInputs));
            }
            #endregion

            //IllogicalValues, ReusedNames, and DefaultValues are handled by the individual handlers

            return ret;
        }
        private ValidationReport ValidateCommands(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "Commands");

            #region Null Validation
            if (null == Commands)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < Commands.Count; ++i)
                    if (null == Commands[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.Augment(Commands[i].Validate(settings, workingPath));//individual validation
            #endregion

            #region Name Validation
            //group validation (name validation)
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                var nameIssues = ValidationUtils.ReusedNamesValidation(Commands, workingPath);
                if (null != nameIssues)
                    ret.ValidationIssues.AddRange(nameIssues);
            }
            #endregion

            return ret;
        }
        private ValidationReport ValidateJoysticks(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "Joysticks");

            #region Null Validation
            if (null == Joysticks)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < Joysticks.Count; ++i)
                    if (null == Joysticks[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.Augment(Joysticks[i].Validate(settings, workingPath));//individual validation
            #endregion

            #region Name Validation
            //group validation (name validation)
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                var nameIssues = ValidationUtils.ReusedNamesValidation(Joysticks, workingPath);
                if (null != nameIssues)
                    ret.ValidationIssues.AddRange(nameIssues);
            }
            #endregion

            #region Reused ID Validation
            if (settings.Contains(ValidationSetting.ReusedIds))
            {
                var nameMap = new Dictionary<uint, List<string>>();
                ValidationUtils.GetNamesOfId(Joysticks, workingPath, ref nameMap);
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(nameMap));
            }
            #endregion

            return ret;
        }
        private ValidationReport ValidateTriggers(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "Triggers");

            #region Null Validation
            if (null == Triggers)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < Triggers.Count; ++i)
                    if (null == Triggers[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.Augment(Triggers[i].Validate(settings, workingPath));//individual validation
            #endregion
            
            #region Name Validation
            //group validation (name validation)
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                var nameIssues = ValidationUtils.ReusedNamesValidation(Triggers, workingPath);
                if (null != nameIssues)
                    ret.ValidationIssues.AddRange(nameIssues);
            }
            #endregion

            #region Reused Id Validation
            //this validates the reuse of joystick buttons; use command groups if multiple things happen on a button press
            if (settings.Contains(ValidationSetting.ReusedIds))
            {
                var joystickTriggers = Triggers.OfType<OzJoystickTriggerData>().ToList();
                var idMap = new Dictionary<uint, List<string>>();
                ValidationUtils.GetNamesOfId(joystickTriggers, workingPath, ref idMap);
                ret.ValidationIssues.AddRange(ValidationUtils.GetIssuesFromIdMap(idMap));
            }
            #endregion

            return ret;
        }
        #endregion
    }
}
