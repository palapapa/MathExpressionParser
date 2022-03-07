using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// A <see langword="delegate"/> used to calculate the value of a <see cref="FunctionalOperator"/>.
/// </summary>
/// <param name="arguments">An <see cref="IList{T}"/> of arguments passed to a <see cref="FunctionalOperator"/> in a <see cref="MathExpression"/>.</param>
/// <returns>The value of a <see cref="FunctionalOperator"/></returns>
public delegate string FunctionalOperatorDelegate(IList<string> arguments);