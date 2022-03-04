namespace MathExpressionParser;

internal record class PostfixUnaryOperatorToken : Token
{
    public PostfixUnaryOperatorToken(Token token) : base(token)
    {
    }
}
