namespace MathExpressionParser;

internal record class PrefixUnaryOperatorToken : Token
{
    public PrefixUnaryOperatorToken(Token token) : base(token)
    {
    }
}
