namespace MathExpressionParser;

/// <summary>
/// Represents the precedence of an <see cref="Operator"/>. An <see langword="enum"/> option with a higher value is parsed first.
/// </summary>
public enum OperatorPrecedence
{
    /// <summary>
    /// This precedence level is parsed fourth.
    /// </summary>
    Additive,

    /// <summary>
    /// This precedence level is parsed third.
    /// </summary>
    Multiplicative,

    /// <summary>
    /// This precedence level is parsed second.
    /// </summary>
    Unary,

    /// <summary>
    /// This precedence level is parsed first.
    /// </summary>
    Exponentiation
}
