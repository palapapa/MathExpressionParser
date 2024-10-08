﻿using System;

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
    /// <exception cref="ArgumentNullException">When this is set to <see langword="null"/>.</exception>
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(Name));
    }

    /// <summary>
    /// Base constructor for all derived <see cref="Operator"/>s.
    /// </summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <see langword="null"/>.</exception>
    public Operator(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
