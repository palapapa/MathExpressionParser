namespace MathExpressionParser;

/// <summary>
/// Exposes common properties of an operator in an <see cref="MathExpression"/>.
/// </summary>
public interface IOperator
{
    /// <summary>
    /// The <see cref="string"/> representation of the this <see cref="IOperator"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The precedence of this <see cref="IOperator"/>.
    /// </summary>
    public OperatorPrecedence Precedence { get; set; }

    /// <summary>
    /// The associativity of this <see cref="IOperator"/>.
    /// </summary>
    public OperatorAssociativity Associativity { get; set; }
}
