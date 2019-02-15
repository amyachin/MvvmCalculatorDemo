using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCalculatorDemo.Model
{
    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public static class OperationTypeExtensions
    {
        public static string ToDisplayString(this OperationType operation)
        {

            switch (operation)
            {
                case OperationType.Add:
                    return "+";

                case OperationType.Divide:
                    return "/";

                case OperationType.Multiply:
                    return "*";

                case OperationType.Subtract:
                    return "-";

                default:
                    return "?";
            }
        }
    }
}
