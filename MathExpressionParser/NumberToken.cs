namespace MathExpressionParser;

internal record class NumberToken : Token
{
    public double Value { get; }
    
    public NumberToken(string content, int position, double value) : base(content, position)
    {
        Value = value;
    }

    public static explicit operator double(NumberToken token)
    {
        return token.Value;
    }
}
