namespace MathExpressionParser;

internal record class PostfixUnaryOperatorToken : Token
{
    public PostfixUnaryOperatorToken(string content, int position) : base(content, position)
    {
    }
}
