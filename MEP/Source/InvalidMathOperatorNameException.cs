using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when the <see cref="MathOperator"/>s supplied to a parsing method contains a <see cref="MathOperator"/> with an invalid name.
/// </summary>
[Serializable]
public class InvalidMathOperatorNameException : ArgumentException
{
    public InvalidMathOperatorNameException() : base()
    {

    }

    public InvalidMathOperatorNameException(string message) : base(message)
    {

    }

    public InvalidMathOperatorNameException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public InvalidMathOperatorNameException(string message, string paramName) : base(message, paramName)
    {

    }

    public InvalidMathOperatorNameException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected InvalidMathOperatorNameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
