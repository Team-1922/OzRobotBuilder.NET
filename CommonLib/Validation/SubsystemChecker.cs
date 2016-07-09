using CommonLib.Model.CompositeTypes;
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
    }
}
