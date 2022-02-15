using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when the expression supplied to a parsing method contains an unmatched parenthesis.
/// </summary>
[Serializable]
public class UnmatchedParenthesisException : ArgumentException
{
    public UnmatchedParenthesisException() : base()
    {

    }

    public UnmatchedParenthesisException(string message) : base(message)
    {

    }

    public UnmatchedParenthesisException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public UnmatchedParenthesisException(string message, string paramName) : base(message, paramName)
    {

    }

    public UnmatchedParenthesisException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected UnmatchedParenthesisException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
