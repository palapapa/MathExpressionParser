namespace MathExpressionParser;

internal record class BinaryOperatorToken : Token
{
    public BinaryOperatorToken(Token token) : base(token)
    {
    }
}
