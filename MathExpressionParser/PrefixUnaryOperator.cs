using System;

namespace MathExpressionParser;

internal class PrefixUnaryOperator : Operator
{
    private PrefixUnaryOperatorDelegate calculate;
    public PrefixUnaryOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }

    public PrefixUnaryOperator(string name, OperatorPrecedence precedence, PrefixUnaryOperatorDelegate calculate) : base(name, precedence)
    {
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
    }
}