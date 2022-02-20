using System;

namespace MathExpressionParser;

/// <summary>
/// Represents a constant in a <see cref="MathExpression"/>.
/// </summary>
public class ConstantOperator : Operator
{
    /// <summary>
    /// The value this <see cref="ConstantOperator"/> holds.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Initailizes a new instance of <see cref="ConstantOperator"/>.
    /// </summary>
    /// <param name="name"><inheritdoc cref="Operator.Name" path="/summary"/></param>
    /// <param name="precedence"><inheritdoc cref="Operator.Precedence" path="/summary"/></param>
    /// <param name="value">The value of this <see cref="ConstantOperator"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="name"/> is null.</exception>
    public ConstantOperator(string name, double value, OperatorPrecedence precedence) : base(name, precedence)
    {
        Value = value;
    }
}