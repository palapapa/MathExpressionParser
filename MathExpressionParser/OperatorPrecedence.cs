namespace MathExpressionParser;

/// <summary>
/// Represents the precedence of an <see cref="IOperator"/>. An <see langword="enum"/> option with a higher value is parsed first.
/// </summary>
public enum OperatorPrecedence
{
    Additive,
    Multiplicative,
    Unary,
    Exponentiation,
    Function,
    Constant
}
