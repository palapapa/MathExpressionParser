using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when, after a parsing method ends, the expression still couldn't be reduced to a single number.
/// </summary>
[Serializable]
public class InvalidExpressionException : ArgumentException
{
    public InvalidExpressionException() : base()
    {

    }

    public InvalidExpressionException(string message) : base(message)
    {

    }

    public InvalidExpressionException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public InvalidExpressionException(string message, string paramName) : base(message, paramName)
    {

    }

    public InvalidExpressionException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected InvalidExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
