namespace MathExpressionParser;

internal record class ClosingParenthesisToken : Token
{
    public ClosingParenthesisToken(int position) : base(",", position)
    {
    }
}
