using System;

namespace MathExpressionParser;

internal record class FunctionOperatorToken : Token
{
    public int ArgumentCount { get; init; }

    public FunctionOperatorToken(Token token, int argumentCount) : base(token)
    {
        ArgumentCount = argumentCount;
    }
}
