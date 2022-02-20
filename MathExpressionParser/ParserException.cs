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
    /// <param name="message"><inheritdoc cref="ParserException(string)" path="/param[@name='message']"/></param>
    /// <param name="innerException"><inheritdoc cref="Exception(string?, Exception?)" path="/param[@name='innerException']"/></param>
    public ParserException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParserException"/> with a specified error message and a <see cref="ParserExceptionContext"/> with information about this exception.
    /// </summary>
    /// <param name="message"><inheritdoc cref="ParserException(string)" path="/param[@name='message']"/></param>
    /// <param name="context">A <see cref="ParserExceptionContext"/> with information about this exception.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="context"/> is <see langword="null"/>.</exception>
    public ParserException(string message, ParserExceptionContext context) : base(message)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParserException"/> with a specified error message, a <see cref="ParserExceptionContext"/> with information about this exception, 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message"><inheritdoc cref="ParserException(string)" path="/param[@name='message']"/></param>
    /// <param name="context"><inheritdoc cref="ParserException(string, ParserExceptionContext)" path="/param[@name='context']"/></param>
    /// <param name="innerException"><inheritdoc cref="Exception(string?, Exception?)" path="/param[@name='innerException']"/></param>
    /// <exception cref="ArgumentNullException"><inheritdoc cref="ParserException(string, ParserExceptionContext)" path="/exception[@cref='ArgumentNullException']"/></exception>
    public ParserException(string message, ParserExceptionContext context, Exception innerException) : base(message, innerException)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ArgumentNullException.ThrowIfNull(info, nameof(info));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        Context = (ParserExceptionContext)info.GetValue("Context", typeof(ParserExceptionContext));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        ArgumentNullException.ThrowIfNull(info, nameof(info));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        info.AddValue("Context", Context, typeof(ParserExceptionContext));
    }
}
