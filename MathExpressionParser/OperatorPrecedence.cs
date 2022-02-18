namespace MathExpressionParser;

/// <summary>
/// Represents the precedence of an <see cref="IOperator"/>. An <see langword="enum"/> option with a higher value is parsed first.
/// </summary>
public enum OperatorPrecedence
{
    /// <summary>
    /// This precedence level is parsed sixth.
    /// </summary>
    Additive,
    /// <summary>
    /// This precedence level is parsed fifth.
    /// </summary>
    Multiplicative,
    /// <summary>
    /// This precedence level is parsed fourth.
    /// </summary>
    Unary,
    /// <summary>
    /// This precedence level is parsed third.
    /// </summary>
    Exponentiation,
    /// <summary>
    /// This precedence level is parsed second.
    /// </summary>
    Function,
    /// <summary>
    /// This precedence level is parsed first.
    /// </summary>
    Constant
}
