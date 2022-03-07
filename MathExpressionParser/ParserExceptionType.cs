namespace MathExpressionParser;

/// <summary>
/// Represents the kind of error that caused a <see cref="ParserException"/> to be thrown.
/// </summary>
public enum ParserExceptionType
{
    /// <summary>
    /// Represents a situation where a number with an invalid format was found in a <see cref="MathExpression"/>.
    /// </summary>
    InvalidNumberFormat,
    /// <summary>
    /// Represents a situation where either too many or too few arguments were passed to a <see cref="FunctionalOperator"/>.
    /// </summary>
    IncorrectArgumentCount
}
