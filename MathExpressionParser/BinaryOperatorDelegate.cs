namespace MathExpressionParser;

/// <summary>
/// A delegate used to compute the result of a <see cref="BinaryOperator"/>.
/// </summary>
/// <param name="left">The left operand of a <see cref="BinaryOperator"/>.</param>
/// <param name="right">The right operand of a <see cref="BinaryOperator"/>.</param>
/// <returns>The computed result of a <see cref="BinaryOperator"/>.</returns>
internal delegate double BinaryOperatorDelegate(double left, double right);