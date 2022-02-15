using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when the collection of <see cref="MathOperator"/>s supplied to a parsing method contains <see cref="MathOperator"/>s 
/// with the same name and type or when it contains a <see cref="ConstantMathOperator"/> with an overload.
/// </summary>
[Serializable]
public class DuplicateMathOperatorException : ArgumentException
{
    public DuplicateMathOperatorException() : base()
    {

    }

    public DuplicateMathOperatorException(string message) : base(message)
    {

    }

    public DuplicateMathOperatorException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public DuplicateMathOperatorException(string message, string paramName) : base(message, paramName)
    {

    }

    public DuplicateMathOperatorException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected DuplicateMathOperatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
