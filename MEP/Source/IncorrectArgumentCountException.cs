using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown by a <see cref="FunctionMathOperator"/> when the arguments provided isn't the right number.
/// </summary>
[Serializable]
public class IncorrectArgumentCountException : ArgumentException
{
    public IncorrectArgumentCountException() : base()
    {

    }

    public IncorrectArgumentCountException(string message) : base(message)
    {

    }

    public IncorrectArgumentCountException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public IncorrectArgumentCountException(string message, string paramName) : base(message, paramName)
    {

    }

    public IncorrectArgumentCountException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected IncorrectArgumentCountException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
