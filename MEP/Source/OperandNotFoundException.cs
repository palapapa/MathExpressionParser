using System;
using System.Runtime.Serialization;

namespace MEP;

/// <summary>
/// An <see cref="Exception"/> thrown when a suitable overload of a <see cref="MathOperator"/> is found but its operands cannot be found.
/// </summary>
[Serializable]
public class OperandNotFoundException : ArgumentException
{
    public OperandNotFoundException() : base()
    {

    }

    public OperandNotFoundException(string message) : base(message)
    {

    }

    public OperandNotFoundException(string message, Exception innerException) : base(message, innerException)
    {

    }

    public OperandNotFoundException(string message, string paramName) : base(message, paramName)
    {

    }

    public OperandNotFoundException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {

    }

    protected OperandNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
