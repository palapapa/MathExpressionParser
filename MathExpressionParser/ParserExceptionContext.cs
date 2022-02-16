namespace MathExpressionParser;

/// <summary>
/// A record class that contains additional information regarding a <see cref="ParserException"/> instance.
/// </summary>
/// <param name="ErrorPosition">The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs.</param>
public record class ParserExceptionContext(int ErrorPosition)
{
    /// <summary>
    /// The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs.
    /// </summary>
    public int ErrorPosition { get; init; } = ErrorPosition;
}
