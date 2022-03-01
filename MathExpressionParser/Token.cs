using System;

namespace MathExpressionParser;

internal record class Token
{
    private string content;
    public string Content
    {
        get => content;
        init => content = value ?? throw new ArgumentNullException(nameof(Content));
    }
    public int Position { get; init; }

    public Token(string content, int position)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        Position = position;
    }

    public override string ToString()
    {
        return Content;
    }

    public static implicit operator string(Token token)
    {
        return token.Content;
    }
}