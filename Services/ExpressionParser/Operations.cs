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
        public static List<BinaryOperationDouble> DoubleOperations { get; } = new List<BinaryOperationDouble>()
        {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division(),
            new Modulus(),
            new Power(),
            new Greater(),
            new GreaterOrEqual(),
            new Less(),
            new LessOrEqual(),
            new IsEqual()
        };
        public static List<BinaryOperationBool> BoolOperations { get; } = new List<BinaryOperationBool>()
        {
            new BooleanOr(),
            new BooleanAnd()
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

    #region Boolean Operaitons
    internal class BooleanOr : BinaryOperationBool
    {
        public override string Name => "||";
        public override bool Perform(bool input1, bool input2) => input1 || input2;
    }
    internal class BooleanAnd : BinaryOperationBool
    {
        public override string Name => "&&";
        public override bool Perform(bool input1, bool input2) => input1 && input2;
    }

    #endregion

    #region Double Operations
    /// <summary>
    /// This is not technically a binary operation, however it does need to have the "Priority" property to work correctly
    /// </summary>
    internal class UnaryMinus : BinaryOperationDouble
    {
        public override string Name => "-";
        public override OperationPriority Priority => OperationPriority.MultDiv;
        public override double Perform(double input1, double input2) => -1 * input2;
    }
    internal class Addition : BinaryOperationDouble
    {
        public override string Name => "+";
        public override OperationPriority Priority => OperationPriority.AddSub;
        public override double Perform(double input1, double input2) => input1 + input2;
    }
    internal class Subtraction : BinaryOperationDouble
    {
        public override string Name => "-";
        public override OperationPriority Priority => OperationPriority.AddSub;
        public override double Perform(double input1, double input2) => input1 - input2;
    }
    internal class Multiplication : BinaryOperationDouble
    {
        public override string Name => "*";
        public override OperationPriority Priority => OperationPriority.MultDiv;
        public override double Perform(double input1, double input2) => input1 * input2;
    }
    internal class Division : BinaryOperationDouble
    {
        public override string Name => "/";
        public override OperationPriority Priority => OperationPriority.MultDiv;
        public override double Perform(double input1, double input2) => input1 / input2;
    }
    internal class Modulus : BinaryOperationDouble
    {
        public override string Name => "%";
        public override OperationPriority Priority => OperationPriority.MultDiv;
        public override double Perform(double input1, double input2) => input1 % input2;
    }
    internal class Power : BinaryOperationDouble
    {
        public override string Name => "^";
        public override OperationPriority Priority => OperationPriority.Exponent;
        public override double Perform(double input1, double input2) => Math.Pow(input1, input2);
    }
    internal class Greater : BinaryOperationDouble
    {
        public override string Name => ">";
        public override OperationPriority Priority => OperationPriority.Boolean;
        public override double Perform(double input1, double input2) => input1 > input2 ? 1 : 0;
    }
    internal class GreaterOrEqual : BinaryOperationDouble
    {
        public override string Name => ">=";
        public override OperationPriority Priority => OperationPriority.Boolean;
        public override double Perform(double input1, double input2) => input1 >= input2 ? 1 : 0;
    }
    internal class Less : BinaryOperationDouble
    {
        public override string Name => "<";
        public override OperationPriority Priority => OperationPriority.Boolean;
        public override double Perform(double input1, double input2) => input1 < input2 ? 1 : 0;
    }
    internal class LessOrEqual : BinaryOperationDouble
    {
        public override string Name => "<=";
        public override OperationPriority Priority => OperationPriority.Boolean;
        public override double Perform(double input1, double input2) => input1 <= input2 ? 1 : 0;
    }
    internal class IsEqual : BinaryOperationDouble
    {
        public override string Name => "==";
        public override OperationPriority Priority => OperationPriority.Boolean;
        public override double Perform(double input1, double input2) => input1 == input2 ? 1 : 0;
    }
    #endregion
}
