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
    /// Represents a situation where too many arguments were passed to a <see cref="FunctionOperator"/>.
    /// </summary>
    TooManyArguments,
    /// <summary>
    /// Represents a situation where too few arguments were passed to a <see cref="FunctionOperator"/>.
    /// </summary>
    TooFewArguments
}
