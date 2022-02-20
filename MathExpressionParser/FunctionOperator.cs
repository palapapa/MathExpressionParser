using System;
using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// Represents a function in a <see cref="MathExpression"/>.
/// </summary>
public class FunctionOperator : Operator
{
    private FunctionOperatorDelegate calculate;
    /// <summary>
    /// <inheritdoc cref="FunctionOperatorDelegate" path="/summary"/>
    /// </summary>
    public FunctionOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }
    private IList<int> argumentCounts;
    /// <summary>
    /// The possible number of arguments this <see cref="FunctionOperator"/> can take. If this has 0 elements, this <see cref="FunctionOperator"/> can take any number of arguments.
    /// </summary>
    public IList<int> ArgumentCounts
    {
        get => argumentCounts;
        set => argumentCounts = value ?? throw new ArgumentNullException(nameof(ArgumentCounts));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="FunctionOperator"/>.
    /// </summary>
    /// <param name="name"><inheritdoc cref="Operator.Name" path="/summary"/></param>
    /// <param name="precedence"><inheritdoc cref="Operator.Precedence" path="/summary"/></param>
    /// <param name="calculate"><inheritdoc cref="Calculate" path="/summary"/></param>
    /// <param name="argumentCounts"><inheritdoc cref="ArgumentCounts" path="/summary"/></param>
    /// <exception cref="ArgumentNullException">When either <paramref name="name"/> or <paramref name="calculate"/> is null.</exception>
    public FunctionOperator(string name, OperatorPrecedence precedence, FunctionOperatorDelegate calculate, params int[] argumentCounts) : base(name, precedence)
    {
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
        ArgumentCounts = argumentCounts ?? throw new ArgumentNullException(nameof(argumentCounts));
    }
}