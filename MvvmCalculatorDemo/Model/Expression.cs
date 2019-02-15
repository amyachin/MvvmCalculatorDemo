using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmCalculatorDemo.Model
{


    public class Expression
    {
        public Expression(double left, double right, OperationType operation)
        {
            Left = left;
            Right = right;
            Operation = operation;
        }

        public double Left { get; }

        public double Right { get; }

        public OperationType Operation { get; }

        public double Evaluate()
        {


            switch (Operation)
            {
                case OperationType.Add:
                    return Left + Right;

                case OperationType.Divide:
                    return Left / Right;

                case OperationType.Multiply:
                    return Left * Right;

                case OperationType.Subtract:
                    return Left - Right;

                default:
                    return double.NaN;
            }
        }

        public override string ToString()
        {
            string operationText = Operation.ToDisplayString();
            return string.Format("{0}{1}{2}", Left, operationText, Right);
        }
    }
}
