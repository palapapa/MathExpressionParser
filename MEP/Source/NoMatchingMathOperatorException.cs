using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when the expression supplied to a parsing method contains a <see cref="MathOperator"/> whose suitable overload is not found in the supplied collection of <see cref="MathOperator"/>s.
/// </summary>
[Serializable]
public class NoMatchingMathOperatorException : ArgumentException
{
    public NoMatchingMathOperatorException() : base()
    {

    }

    public NoMatchingMathOperatorException(string message) : base(message)
    {

    }

    public NoMatchingMathOperatorException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public NoMatchingMathOperatorException(string message, string paramName) : base(message, paramName)
    {

    }

    public NoMatchingMathOperatorException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected NoMatchingMathOperatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
