using System;
using MvvmCalculatorDemo.Model;

namespace MvvmCalculatorDemo.Design
{
    public class DesignCalculator : ICalculator
    {
        public DesignCalculator()
        {
            Expression = new Expression(200, 5, OperationType.Multiply);
        }

        public Expression Expression { get; private set; }

        public double? CurrentValue => Expression.Evaluate();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void PushOperation(OperationType operation)
        {
            throw new NotImplementedException();
        }

        public void PushValue(double value)
        {
            throw new NotImplementedException();
        }
    }
}