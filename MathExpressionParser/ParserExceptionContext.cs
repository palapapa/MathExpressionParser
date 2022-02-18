namespace MathExpressionParser;

/// <summary>
/// A record class that contains additional information regarding a <see cref="ParserException"/> instance.
/// </summary>
/// <param name="ErrorPosition">The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs.</param>
/// <param name="ParserExceptionType">The type of error that happened.</param>
public record class ParserExceptionContext(int ErrorPosition, ParserExceptionType ParserExceptionType)
{
    /// <summary>
    /// The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs.
    /// </summary>
    public int ErrorPosition { get; init; } = ErrorPosition;

    /// <summary>
    /// The kind of error that caused a <see cref="ParserException"/> to be thrown.
    /// </summary>
    public ParserExceptionType Type { get; init; } = ParserExceptionType;
}
