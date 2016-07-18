using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators.SingleOperators
{
    public sealed class SubstractOperator : IBinaryOperator
    {
        public int Precedence { get; set; }
        public OperatorAssociativity OperatorAssociativity { get; set; }
        public string OperatorNotationInput { get; set; }
        public string OperatorNotationOutput { get; set; }

        public decimal Calculate(decimal operand1, decimal operand2)
        {
            return operand1 - operand2;
        }

        public SubstractOperator()
        {
            Precedence = 4;
            OperatorNotationOutput = OperatorNotationInput = "-";
            OperatorAssociativity = OperatorAssociativity.Left;
        }
    }
}
