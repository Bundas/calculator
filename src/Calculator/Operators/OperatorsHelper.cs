using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators
{
    public static class OperatorsHelper
    {
        public static int Compare(IBaseOperator o1, IBaseOperator o2)
        {
            if (o1.OperatorNotationOutput.Equals(o2.OperatorNotationOutput) &&
                o1.OperatorAssociativity == OperatorAssociativity.Right)
                return -1;

            return o1.Precedence >= o2.Precedence ? 1 : -1;
        }

        public static bool IsTokenDigit(string token)
        {
            return Regex.IsMatch(token, @"\d|\.");
        }

        public static bool IsTokenName(string token)
        {
            return Regex.IsMatch(token, @"[a-zA-Z0-9]");
        }

        public static bool IsTokenFunction(string op, IEnumerable<IUnaryOperator> unaryOperators)
        {
            var oper = unaryOperators.FirstOrDefault(o => o.OperatorNotationInput == op);

            return oper?.UnaryOperatorType == UnaryOperatorType.Function;
        }

        public static bool IsTokenUnary(string op, IEnumerable<IUnaryOperator> unaryOperators)
        {
            return unaryOperators.Any(o => o.OperatorNotationInput.Equals(op));
        }

        public static bool IsTokenBinary(string op, IEnumerable<IBinaryOperator> binaryOperators)
        {
            return binaryOperators.Any(o => o.OperatorNotationInput.Equals(op));
        }

        public static string ConvertOperator(IBaseOperator o)
        {
            return o.OperatorNotationOutput;
        }
    }
}
