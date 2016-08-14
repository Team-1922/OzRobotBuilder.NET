using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser.Operations
{
    /// <summary>
    /// This represents the core operations with the special mathmatical syntax (i.e. 2 + 4 instead of +(2,4))
    /// </summary>
    internal static class OperationInstances
    {
        public static List<BinaryOperationDouble> DoubleOperations { get; } = new List<BinaryOperationDouble>() {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division(),
            new Modulus(),
            new Power()
        };
    }

    internal class ClampOperation : FuncStyleOperation
    {
        public override string Name
        {
            get
            {
                return "clamp";
            }
        }

        public override uint ParamCount
        {
            get
            {
                return 3;
            }
        }

        protected override double PerformInternal(List<double> param)
        {
            return Math.Min(Math.Max(param[0], param[1]),param[2]);
        }
    }

    #region Double Operations
    /// <summary>
    /// This is not technically a binary operation, however it does need to have the "Priority" property to work correctly
    /// </summary>
    internal class UnaryMinus : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "-";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        public override double Perform(double input1, double input2)
        {
            return -1 * input2;
        }
    }
    internal class Addition : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "+";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.AddSub;
            }
        }

        public override double Perform(double input1, double input2) => input1 + input2;
    }
    internal class Subtraction : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "-";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.AddSub;
            }
        }

        public override double Perform(double input1, double input2) => input1 - input2;
    }
    internal class Multiplication : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "*";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        public override double Perform(double input1, double input2) => input1 * input2;
    }
    internal class Division : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "/";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        public override double Perform(double input1, double input2) => input1 / input2;
    }
    internal class Modulus : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "%";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        public override double Perform(double input1, double input2) => input1 % input2;
    }
    internal class Power : BinaryOperationDouble
    {
        public override string Name
        {
            get
            {
                return "^";
            }
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.Exponent;
            }
        }

        public override double Perform(double input1, double input2) => Math.Pow(input1, input2);
    }
    #endregion
}
