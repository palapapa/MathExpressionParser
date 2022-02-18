namespace MathExpressionParser;

/// <summary>
/// Represents the kind of error that caused a <see cref="ParserException"/> to be thrown.
/// </summary>
public enum ParserExceptionType
{
    /// <summary>
    /// Represents a situation where a number with an invalid format is found in a <see cref="MathExpression"/>.
    /// </summary>
    InvalidNumberFormat
}
