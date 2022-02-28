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
    /// <summary>
    /// The order in which this <see cref="Operator"/> will be parsed.
    /// </summary>
    public OperatorPrecedence Precedence { get; set; }

    public PrefixUnaryOperator(string name, OperatorPrecedence precedence, PrefixUnaryOperatorDelegate calculate) : base(name)
    {
        Precedence = precedence;
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
    }
}