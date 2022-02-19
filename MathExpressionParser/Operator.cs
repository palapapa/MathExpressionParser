namespace MathExpressionParser;

/// <summary>
/// Base class for different types of operators in an <see cref="MathExpression"/>.
/// </summary>
public abstract class Operator
{
    /// <summary>
    /// The <see cref="string"/> representation of the this <see cref="Operator"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The precedence of this <see cref="Operator"/>.
    /// </summary>
    public OperatorPrecedence Precedence { get; set; }

    /// <summary>
    /// The associativity of this <see cref="Operator"/>.
    /// </summary>
    public OperatorAssociativity Associativity { get; set; }
}
