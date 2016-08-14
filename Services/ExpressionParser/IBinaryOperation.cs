using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    interface IBinaryOperationDouble : IOperationDouble
    {        
        double Perform(double input1, double input2);
    }
    interface IBinaryOperationBool : IOperationBool
    {
        bool Perform(bool input1, bool input2);
    }
}
