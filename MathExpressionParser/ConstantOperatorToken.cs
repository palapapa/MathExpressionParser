namespace MathExpressionParser;

internal record class ConstantOperatorToken : Token
{
    public ConstantOperatorToken(string content, int position) : base(content, position)
    {
    }
}
