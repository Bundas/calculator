# Burda calculator v1.0

The app parses math expressions and calculates results. It's easy to extend. Just create new operator which implements either `IBinaryOperator` or `IUnaryOperator` interface located in `Calculator.Operators.Interfaces`.

<h3>Example binary operator:</h3>
```
public sealed class AddOperator : IBinaryOperator
{
    public int Precedence { get; set; }
    public OperatorAssociativity OperatorAssociativity { get; set; }
    public string OperatorNotationInput { get; set; }
    public string OperatorNotationOutput { get; set; }

    public decimal Calculate(decimal operand1, decimal operand2)
    {
        return operand1 + operand2;
    }

    public AddOperator()
    {
        Precedence = 4;
        OperatorNotationOutput = OperatorNotationInput = "+";
        OperatorAssociativity = OperatorAssociativity.Left;
    }
}
```

<h3>Example unary operator:</h3>
```
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

```
