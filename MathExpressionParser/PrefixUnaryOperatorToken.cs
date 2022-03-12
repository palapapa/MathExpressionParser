namespace MathExpressionParser;

internal record class PrefixUnaryOperatorToken : Token
{
    public PrefixUnaryOperatorToken(string content, int position) : base(content, position)
    {
    }
}
