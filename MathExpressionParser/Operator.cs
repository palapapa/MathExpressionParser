using System;

namespace MathExpressionParser;

/// <summary>
/// Base class for different types of operators in an <see cref="MathExpression"/>.
/// </summary>
public abstract class Operator
{
    private string name;
    /// <summary>
    /// The <see cref="string"/> representation of the this <see cref="Operator"/>.
    /// </summary>
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(Name));
    }

    /// <summary>
    /// The order in which this <see cref="Operator"/> will be parsed.
    /// </summary>
    public OperatorPrecedence Precedence { get; set; }

    public Operator(string name, OperatorPrecedence precedence)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Precedence = precedence;
    }
}
