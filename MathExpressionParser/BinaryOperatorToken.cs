namespace MathExpressionParser;

internal record class BinaryOperatorToken : Token
{
    public BinaryOperatorToken(string content, int position) : base(content, position)
    {
    }
}
