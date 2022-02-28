using System;

namespace MathExpressionParser;

internal record class FunctionToken : Token
{
    public int ArgumentCount { get; init; }

    public FunctionToken(Token token, int argumentCount) : base(token)
    {
        ArgumentCount = argumentCount;
    }

    public static implicit operator string(FunctionToken token)
    {
        return token.Content;
    }
}
