using System;

namespace MathExpressionParser;

/// <summary>
/// Represents a operator that takes the left and right tokens as its operands.
/// </summary>
internal class BinaryOperator : Operator
{
    public OperatorAssociativity Associativity { get; set; }
    private BinaryOperatorDelegate calculate;
    public BinaryOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }

    public BinaryOperator(string name, OperatorPrecedence precedence, OperatorAssociativity associativity, BinaryOperatorDelegate calculate) : base(name, precedence)
    {
        Associativity = associativity;
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
    }
}