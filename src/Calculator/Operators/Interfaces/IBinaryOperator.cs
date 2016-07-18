namespace Calculator.Operators.Interfaces
{
    public interface IBinaryOperator : IBaseOperator
    {
        decimal Calculate(decimal operand1, decimal operand2);
    }
}
