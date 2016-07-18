using System;
using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators.SingleOperators
{
    public sealed class DecadicLogarithmOperator : IUnaryOperator
    {
        public int Precedence { get; set; }
        public OperatorAssociativity OperatorAssociativity { get; set; }
        public string OperatorNotationInput { get; set; }
        public string OperatorNotationOutput { get; set; }
        public UnaryOperatorType UnaryOperatorType { get; set; }
        public decimal Calculate(decimal operand)
        {
            return (decimal) Math.Log10((double) operand);
        }

        public DecadicLogarithmOperator()
        {
            Precedence = 64;
            OperatorNotationOutput = OperatorNotationInput = "log10";
            UnaryOperatorType = UnaryOperatorType.Function;
            OperatorAssociativity = OperatorAssociativity.Left;
        }
    }
}
