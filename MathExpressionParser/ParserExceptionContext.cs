namespace MathExpressionParser;

/// <summary>
/// A record class that contains additional information regarding a <see cref="ParserException"/> instance.
/// </summary>
/// <param name="Position">The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs. Set this to a value less than zero if it is not applicable.</param>
/// <param name="Type">The type of error that happened.</param>
public record class ParserExceptionContext(int Position, ParserExceptionType Type)
{
    /// <summary>
    /// The position in the <see cref="MathExpression"/> where the <see cref="ParserException"/> occurs. If this is not applicable, it will be less than zero.
    /// </summary>
    public int Position { get; init; } = Position;

    /// <summary>
    /// The kind of error that caused a <see cref="ParserException"/> to be thrown.
    /// </summary>
    public ParserExceptionType Type { get; init; } = Type;
}
