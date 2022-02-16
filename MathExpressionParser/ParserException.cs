using System;
using System.Runtime.Serialization;

namespace MathExpressionParser;

/// <summary>
/// An <see cref="Exception"/> that is thrown when <see cref="IMathExpression.Evaluate"/> encounters an unexpected situation.
/// </summary>
[Serializable]
public class ParserException : Exception
{
    /// <summary>
    /// Represents extra information regarding this instance of <see cref="ParserException"/>.
    /// </summary>
    public ParserExceptionContext Context { get; }
    
    /// <summary>
    /// Initializes a new instance of <see cref="ParserException"/>.
    /// </summary>
    public ParserException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ParserException"/> with a specified error message.
    /// </summary>
    /// <param name="message">The reason why this <see cref="ParserException"/> was thrown.</param>
    public ParserException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParserException"/> with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The reason why this <see cref="ParserException"/> was thrown.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference(Nothing in Visual Basic) if no inner exception is specified.</param>
    public ParserException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParserException"/> with a specified error message and a <see cref="ParserExceptionContext"/> with information about this exception.
    /// </summary>
    /// <param name="message">The reason why this <see cref="ParserException"/> was thrown.</param>
    /// <param name="context">A <see cref="ParserExceptionContext"/> with information about this exception.</param>
    public ParserException(string message, ParserExceptionContext context) : base(message)
    {
        Context = context;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParserException"/> with a specified error message, a <see cref="ParserExceptionContext"/> with information about this exception, 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The reason why this <see cref="ParserException"/> was thrown.</param>
    /// <param name="context">A <see cref="ParserExceptionContext"/> with information about this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference(Nothing in Visual Basic) if no inner exception is specified.</param>
    public ParserException(string message, ParserExceptionContext context, Exception innerException) : base(message, innerException)
    {
        Context = context;
    }

    protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Context = (ParserExceptionContext)info.GetValue("Context", typeof(ParserExceptionContext));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("Context", Context, typeof(ParserExceptionContext));
    }
}
