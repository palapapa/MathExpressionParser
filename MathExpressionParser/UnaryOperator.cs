namespace MathExpressionParser;

internal abstract class UnaryOperator : Operator
{
    public OperatorPrecedence Precedence { get; set; }

    public UnaryOperator(string name, OperatorPrecedence precedence) : base(name)
    {
        Precedence = precedence;
    }
}
