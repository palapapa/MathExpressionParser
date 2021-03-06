using System;

namespace MathExpressionParser;

/// <summary>
/// Represents a operator that takes the left and right tokens as its operands.
/// </summary>
internal class BinaryOperator : Operator, IOperatorLike
{
    public OperatorAssociativity Associativity { get; set; }

    private BinaryOperatorDelegate calculate;

    public BinaryOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }

    /// <summary>
    /// The order in which this <see cref="Operator"/> will be parsed.
    /// </summary>
    public OperatorPrecedence Precedence { get; set; }

    public BinaryOperator(string name, OperatorPrecedence precedence, OperatorAssociativity associativity, BinaryOperatorDelegate calculate) : base(name)
    {
        Associativity = associativity;
        Precedence = precedence;
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
    }
}