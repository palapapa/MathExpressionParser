namespace MathExpressionParser;

internal interface IOperatorLike
{
    public OperatorAssociativity Associativity { get; }

    public OperatorPrecedence Precedence { get; }
}
