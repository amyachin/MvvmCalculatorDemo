using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCalculatorDemo.Model
{
    public interface ICalculator
    {
        Expression Expression { get; }

        double? CurrentValue { get; }

        void PushOperation(OperationType operation);

        void PushValue(double value);

        void Flush();

        void Clear();

    }

}
