namespace MathExpressionParser;

internal record class CommaToken : Token
{
    public CommaToken(int position) : base(",", position)
    {
    }
}
