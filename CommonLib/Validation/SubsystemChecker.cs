using CommonLib.Model.CompositeTypes;
using CommonLib.Model.PrimaryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    public class SubsystemChecker : IObjectChecker<OzSubsystemData>
    {
        public ValidationReport ValidateObject(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);

            //this should almost never happen, becauset the RobotDocumentChecker null checks this value before calling this method
            if (null == obj)
            {
                ret.ValidationIssues.Add(new NullValueValidationIssue(workingPath));
                return ret;//return early if obj is null
            }
            workingPath = RobotDocumentChecker.ExtendWorkingPath(workingPath, obj.Name);


            //TODO: finish

            return ret;
        }

        #region Subobject Validation
        private ValidationReport ValidatePWMMotorControllers(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = RobotDocumentChecker.ExtendWorkingPath(workingPath, "PWMMotorControllers");

            #region Null Validation
            if (settings.Contains(ValidationSetting.NullValues))
            {
                //PWM motor controllers
                {
                    string pwmWorkingPath = RobotDocumentChecker.ExtendWorkingPath(workingPath, "PWMMotorControllers");
                    if (null == obj.PWMMotorControllers)
                        ret.ValidationIssues.Add(new NullValueValidationIssue(pwmWorkingPath));
                    else
                        for (int i = 0; i < obj.PWMMotorControllers.Count; ++i)
                            if (null == obj.PWMMotorControllers[i])
                                ret.ValidationIssues.Add(new NullValueValidationIssue(string.Format("{0}[{1}]", workingPath)));
                }
            }
            #endregion
        }
        private ValidationReport ValidateTalonSRXs(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {

        }
        private ValidationReport ValidateAnalogInputDevices(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {

        }
        private ValidationReport ValidateQuadEncoders(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {

        }
        private ValidationReport ValidateDigitalInputs(OzSubsystemData obj, ValidationSettings settings, string workingPath)
        {

        }
        #endregion
    }
}
