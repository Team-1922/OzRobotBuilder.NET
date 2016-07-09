using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    class JsonChecker : IObjectChecker<string>
    {
        public ValidationReport ValidateObject(string obj, ValidationSettings settings)
        {
            ValidationReport ret = new ValidationReport(settings);


            return ret;
        }
    }
}
