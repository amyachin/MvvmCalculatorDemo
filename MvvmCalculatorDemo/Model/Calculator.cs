using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCalculatorDemo.Model
{
    public class Calculator : ICalculator
    {
        public Expression Expression { get; private set; }

        public double? CurrentValue
        {
            get
            {
                return (Expression?.Evaluate()) ?? _value;
            }
        }

        public void PushOperation(OperationType operation)
        {
            if (_operation.HasValue && _value.HasValue)
            {
                UpdateExression(_value.Value);
            }

            _operation = operation;
        }

        public void PushValue(double value)
        {
            if (_operation.HasValue)
            {
                UpdateExression(value);
                _operation = null;
            }

            _value = value;
        }

        public void Flush()
        {
            if (_value.HasValue && _operation.HasValue)
            {
                UpdateExression(_value.Value);
            }

            _operation = null;
            _value = null;
        }

        void UpdateExression(double value )
        {
            double left = CurrentValue.GetValueOrDefault();
            Expression = new Expression(left, value, _operation.Value);
        }


        double? _value;
        OperationType? _operation;

     
        public void Clear()
        {
            Expression = null;
            _operation = null;
            _value = null ;
        }

    }

}
