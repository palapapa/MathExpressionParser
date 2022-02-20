using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// A <see langword="delegate"/> used to calculate the value of a <see cref="FunctionOperator"/>.
/// </summary>
/// <param name="arguments">An <see cref="IList{T}"/> of arguments passed to a <see cref="FunctionOperator"/> in a <see cref="MathExpression"/>.</param>
/// <returns>The value of a <see cref="FunctionOperator"/></returns>
public delegate string FunctionOperatorDelegate(IList<string> arguments);