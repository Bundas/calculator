using System;
using Calculator.Operators.Enums;
using Calculator.Operators.Interfaces;

namespace Calculator.Operators.SingleOperators
{
    public class SinusOperator : IUnaryOperator
    {
        public int Precedence { get; set; }
        public OperatorAssociativity OperatorAssociativity { get; set; }
        public string OperatorNotationInput { get; set; }
        public string OperatorNotationOutput { get; set; }
        public UnaryOperatorType UnaryOperatorType { get; set; }
        public decimal Calculate(decimal operand)
        {
            return (decimal)Math.Sin(Math.PI / 180 * (double)operand);
        }

        public SinusOperator()
        {
            Precedence = 64;
            OperatorAssociativity = OperatorAssociativity.Left;
            OperatorNotationOutput = OperatorNotationInput = "sin";
            UnaryOperatorType = UnaryOperatorType.Function;
        }
    }
}
