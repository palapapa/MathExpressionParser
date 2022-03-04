namespace MathExpressionParser;

internal record class ConstantOperatorToken : Token
{
    public ConstantOperatorToken(Token token) : base(token)
    {
    }
}
