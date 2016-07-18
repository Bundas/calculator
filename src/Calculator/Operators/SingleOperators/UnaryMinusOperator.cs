using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators.SingleOperators
{
    public sealed class UnaryMinusOperator : IUnaryOperator
    {
        public int Precedence { get; set; }
        public OperatorAssociativity OperatorAssociativity { get; set; }
        public string OperatorNotationInput { get; set; }
        public string OperatorNotationOutput { get; set; }
        public UnaryOperatorType UnaryOperatorType { get; set; }
        public decimal Calculate(decimal operand)
        {
            return -operand;
        }

        public UnaryMinusOperator()
        {
            Precedence = 8;
            OperatorNotationInput = "-";
            OperatorNotationOutput = "_";
            UnaryOperatorType = UnaryOperatorType.LeftSide;
            OperatorAssociativity = OperatorAssociativity.Left;
        }
    }
}
