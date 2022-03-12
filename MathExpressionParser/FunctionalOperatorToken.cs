namespace MathExpressionParser;

internal record class FunctionalOperatorToken : Token
{
    public int ArgumentCount { get; init; }

    public FunctionalOperatorToken(string content, int position, int argumentCount) : base(content, position)
    {
        ArgumentCount = argumentCount;
    }
}
