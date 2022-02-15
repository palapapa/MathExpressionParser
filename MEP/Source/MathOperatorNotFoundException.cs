using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when the expression supplied to a parsing method contains a <see cref="MathOperator"/> that is not contained in the collection of <see cref="MathOperator"/>s supplied to the parsing method.
/// </summary>
[Serializable]
public class MathOperatorNotFoundException : ArgumentException
{
    public MathOperatorNotFoundException() : base()
    {

    }

    public MathOperatorNotFoundException(string message) : base(message)
    {

    }

    public MathOperatorNotFoundException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public MathOperatorNotFoundException(string message, string paramName) : base(message, paramName)
    {

    }

    public MathOperatorNotFoundException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected MathOperatorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
