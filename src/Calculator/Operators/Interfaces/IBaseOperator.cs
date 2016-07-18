using Calculator.Operators.Enums;

namespace Calculator.Operators.Interfaces
{
    public interface IBaseOperator
    {
        int Precedence { get; set; }
        OperatorAssociativity OperatorAssociativity { get; set; }
        string OperatorNotationInput { get; set; }
        string OperatorNotationOutput { get; set; }
    }
}
