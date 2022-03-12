namespace MathExpressionParser;

internal record class OpeningParenthesisToken : Token
{
    public OpeningParenthesisToken(int position) : base("(", position)
    {
    }
}
