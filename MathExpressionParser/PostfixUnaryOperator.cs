using System;

namespace MathExpressionParser;

internal class PostfixUnaryOperator : UnaryOperator, IOperatorLike
{
    private PostfixUnaryOperatorDelegate calculate;

    public PostfixUnaryOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }

    public OperatorAssociativity Associativity
    {
        get => OperatorAssociativity.Left;
    }

    public PostfixUnaryOperator(string name, OperatorPrecedence precedence, PostfixUnaryOperatorDelegate calculate) : base(name, precedence)
    {
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
    }
}
