using Calculator.Operators.Enums;

namespace Calculator.Operators.Interfaces
{
    public interface IUnaryOperator : IBaseOperator
    {
        UnaryOperatorType UnaryOperatorType { get; set; }
        decimal Calculate(decimal operand);
    }
}
