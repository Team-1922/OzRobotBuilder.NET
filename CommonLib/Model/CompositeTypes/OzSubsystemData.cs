using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib.Model.PrimaryTypes;
using CommonLib.Interfaces;
using CommonLib.Validation;

namespace CommonLib.Model.CompositeTypes
{
    /// <summary>
    /// A subsystem with all of its possible inputs and outputs
    /// </summary>
    public class OzSubsystemData : INamedClass, IValidatable
    {
        /// <summary>
        /// The Name of the subsystem
        /// </summary>
        public string Name { get; set; } = "SubsystemName";
        /// <summary>
        /// A List of Motor Controller Datas for PWM motor controllers
        /// </summary>
        public UniqueItemList<OzMotorControllerData> PWMMotorControllers { get; set; } = new UniqueItemList<OzMotorControllerData>();
        /// <summary>
        /// A List of Motor Controller Datas for Talon SRX motor controllers
        /// </summary>
        public UniqueItemList<OzTalonSRXData> TalonSRXs { get; set; } = new UniqueItemList<OzTalonSRXData>();
        /// <summary>
        /// A List of Analog Input Datas
        /// </summary>
        public UniqueItemList<OzAnalogInputData> AnalogInputs { get; set; } = new UniqueItemList<OzAnalogInputData>();
        /// <summary>
        /// A List of Quadrature Encoders Datas
        /// </summary>
        public UniqueItemList<OzQuadEncoderData> QuadEncoders { get; set; } = new UniqueItemList<OzQuadEncoderData>();
        /// <summary>
        /// A List of Digital Input Datas (binary inputs)
        /// </summary>
        public UniqueItemList<OzDigitalInputData> DigitalInputs { get; set; } = new UniqueItemList<OzDigitalInputData>();
        /// <summary>
        /// Whether or not the software PID controller is enabled
        /// </summary>
        public bool SoftwarePIDEnabled { get; set; } = false;
        /// <summary>
        /// The Data for the software pid controller
        /// </summary>
        public OzPIDControllerData PIDControllerConfig { get; set; } = new OzPIDControllerData();
        /// <summary>
        /// The data for overriding default methods of a subsystem and adding new ones
        /// </summary>
        public ScriptExtensableData ScriptExtData { get; set; } = new ScriptExtensableData();

        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, Name);

            ret.ValidationIssues.AddRange(ValidatePWMMotorControllers(settings, workingPath));
            ret.ValidationIssues.AddRange(ValidateTalonSRXs(settings, workingPath));
            ret.ValidationIssues.AddRange(ValidateAnalogInputs(settings, workingPath));
            ret.ValidationIssues.AddRange(ValidateQuadEncoders(settings, workingPath));
            ret.ValidationIssues.AddRange(ValidateDigitalInputs(settings, workingPath));
            ret.Augment(PIDControllerConfig.Validate(settings, workingPath));
            ret.Augment(ScriptExtData.Validate(settings, workingPath));

            return ret;
        }

        #region Subobject Validation
        private static List<IValidationIssue> ValidateList<T>(List<T> items, ValidationSettings settings, string workingPath) where T : IValidatable, INamedClass
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "DigitalInputs");

            if (null == items)//container null-check
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
            else
                for (int i = 0; i < items.Count; ++i)
                    if (null == items[i])//individual null-check
                        ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath, i)));
                    else
                        ret.Augment(items[i].Validate(settings, workingPath));//individual validation

            #region Reused Name Validation
            if (settings.Contains(ValidationSetting.ReusedNames))
            {
                var nameIssues = ValidationUtils.ReusedNamesValidation(items, workingPath);
                if (null != nameIssues)
                    ret.ValidationIssues.AddRange(nameIssues);
            }
            #endregion

            //Illogical Values are handled by the individual classes

            return ret;
        }
        private List<IValidationIssue> ValidateDigitalInputs(ValidationSettings settings, string workingPath)
        {
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "DigitalInputs");
            return ValidateList(DigitalInputs, settings, workingPath);
        }
        private List<IValidationIssue> ValidateQuadEncoders(ValidationSettings settings, string workingPath)
        {
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "QuadEncoders");
            return ValidateList(QuadEncoders, settings, workingPath);
        }
        private List<IValidationIssue> ValidateAnalogInputs(ValidationSettings settings, string workingPath)
        {
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "AnalogInputs");
            return ValidateList(AnalogInputs, settings, workingPath);
        }
        private List<IValidationIssue> ValidateTalonSRXs(ValidationSettings settings, string workingPath)
        {
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "TalonSRXs");
            return ValidateList(TalonSRXs, settings, workingPath);
        }
        private List<IValidationIssue> ValidatePWMMotorControllers(ValidationSettings settings, string workingPath)
        {
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "PWMMotorControllers");
            return ValidateList(PWMMotorControllers, settings, workingPath);
        }
        #endregion
    }
}
