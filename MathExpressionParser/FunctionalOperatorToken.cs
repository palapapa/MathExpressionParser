using System;

namespace MathExpressionParser;

internal record class FunctionalOperatorToken : Token
{
    public int ArgumentCount { get; init; }

    public FunctionalOperatorToken(Token token, int argumentCount) : base(token)
    {
        ArgumentCount = argumentCount;
    }
}
