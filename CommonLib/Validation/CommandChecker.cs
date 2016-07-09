using CommonLib.Model.CompositeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    public class CommandChecker : IObjectChecker<OzCommandData>
    {
        public ValidationReport ValidateObject(OzCommandData obj, ValidationSettings settings, string workingPath)
        {
            throw new NotImplementedException();
        }
    }
}
