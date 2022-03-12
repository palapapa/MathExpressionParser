namespace MathExpressionParser;

internal record class NumberToken : Token
{
    public double Value { get; }
    
    public NumberToken(string content, int position) : base(content, position)
    {
        Value = content.ToDouble();
    }

    public static implicit operator double(NumberToken token)
    {
        return token.Value;
    }
}
