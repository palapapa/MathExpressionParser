using System;
using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// Represents a function in a <see cref="MathExpression"/>.
/// </summary>
public class FunctionalOperator : Operator
{
    private FunctionalOperatorDelegate calculate;

    /// <summary>
    /// <inheritdoc cref="FunctionalOperatorDelegate" path="/summary"/>
    /// </summary>
    /// <exception cref="ArgumentNullException">When this is set to <see langword="null"/>.</exception>
    public FunctionalOperatorDelegate Calculate
    {
        get => calculate;
        set => calculate = value ?? throw new ArgumentNullException(nameof(Calculate));
    }

    private IList<int> argumentCounts;

    /// <summary>
    /// The possible number of arguments this <see cref="FunctionalOperator"/> can take. If this has 0 elements, this <see cref="FunctionalOperator"/> can take any number of arguments.
    /// </summary>
    /// <exception cref="ArgumentNullException">When this is set to <see langword="null"/>.</exception>
    public IList<int> ArgumentCounts
    {
        get => argumentCounts;
        set => argumentCounts = value ?? throw new ArgumentNullException(nameof(ArgumentCounts));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="FunctionalOperator"/>.
    /// </summary>
    /// <param name="name"><inheritdoc cref="Operator.Name" path="/summary"/></param>
    /// <param name="precedence"><inheritdoc cref="Operator.Precedence" path="/summary"/></param>
    /// <param name="calculate"><inheritdoc cref="Calculate" path="/summary"/></param>
    /// <param name="argumentCounts"><inheritdoc cref="ArgumentCounts" path="/summary"/></param>
    /// <exception cref="ArgumentNullException">When either <paramref name="name"/> or <paramref name="calculate"/> is null.</exception>
    public FunctionalOperator(string name, FunctionalOperatorDelegate calculate, params int[] argumentCounts) : base(name)
    {
        Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
        ArgumentCounts = argumentCounts ?? throw new ArgumentNullException(nameof(argumentCounts));
    }
}