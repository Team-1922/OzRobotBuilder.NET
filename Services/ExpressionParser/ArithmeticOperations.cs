using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// This represents the core operations with the special mathmatical syntax (i.e. 2 + 4 instead of +(2,4))
    /// </summary>
    internal static class ArithmeticOperations
    {
        public static List<BinaryOperation> Operations { get; } = new List<BinaryOperation>() {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division(),
            new Modulus(),
            new Power() };
    }

    #region Operations
    internal enum OperationPriority
    {
        GroupingSymbols=0,
        Exponent=1,
        MultDiv=2,
        AddSub=3
    }
    internal abstract class BinaryOperation : IOperation
    {
        string _name;
        protected BinaryOperation(string name)
        {
            _name = name;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public uint ParamCount
        {
            get
            {
                return 2;
            }
        }

        public abstract OperationPriority Priority{ get; }

        public double Perform(List<double> param)
        {
            if (param.Count != 2)
                throw new ArgumentException("param count not equal to 2");
            return Perform(param[0], param[1]);
        }
        protected abstract double Perform(double input1, double input2);
    }
    internal class Addition : BinaryOperation
    {
        public Addition() : base("+")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.AddSub;
            }
        }

        protected override double Perform(double input1, double input2) => input1 + input2;
    }
    internal class Subtraction : BinaryOperation
    {
        public Subtraction() : base("-")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.AddSub;
            }
        }

        protected override double Perform(double input1, double input2) => input1 - input2;
    }
    internal class Multiplication : BinaryOperation
    {
        public Multiplication() : base("*")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        protected override double Perform(double input1, double input2) => input1 * input2;
    }
    internal class Division : BinaryOperation
    {
        public Division() : base("/")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        protected override double Perform(double input1, double input2) => input1 / input2;
    }
    internal class Modulus : BinaryOperation
    {
        public Modulus() : base("%")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.MultDiv;
            }
        }

        protected override double Perform(double input1, double input2) => input1 % input2;
    }
    internal class Power : BinaryOperation
    {
        public Power() : base("^")
        {
        }

        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.Exponent;
            }
        }

        protected override double Perform(double input1, double input2) => Math.Pow(input1, input2);
    }
    #endregion
}
