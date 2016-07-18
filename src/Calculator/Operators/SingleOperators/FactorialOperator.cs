using System.Linq;
using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators.SingleOperators
{
    public sealed class FactorialOperator : IUnaryOperator
    {
        public int Precedence { get; set; }
        public OperatorAssociativity OperatorAssociativity { get; set; }
        public string OperatorNotationInput { get; set; }
        public string OperatorNotationOutput { get; set; }
        public UnaryOperatorType UnaryOperatorType { get; set; }
        public decimal Calculate(decimal operand)
        {
            return Factorial((int) operand);
        }

        private static int Factorial(int i)
        {
            return i < 0 ? -1 : i == 0 || i == 1 ? 1 : Enumerable.Range(1, i).Aggregate((counter, value) => counter * value);
        }

        public FactorialOperator()
        {
            Precedence = 48;
            OperatorNotationOutput = OperatorNotationInput = "!";
            UnaryOperatorType = UnaryOperatorType.RightSide;
            OperatorAssociativity = OperatorAssociativity.Left;
        }
    }
}
