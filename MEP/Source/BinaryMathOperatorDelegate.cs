namespace MEP;

/// <summary>
/// A delegate used to compute the result of a <see cref="BinaryMathOperator"/>.
/// </summary>
/// <param name="left">The left operand of a <see cref="BinaryMathOperator"/>. This is a string representation of a <see cref="double"/>.</param>
/// <param name="right">The right operand of a <see cref="BinaryMathOperator"/>. This is a string representation of a <see cref="double"/>.</param>
/// <returns>The computed result of a <see cref="BinaryMathOperator"/>. This is a string representation of a <see cref="double"/>.</returns>
public delegate string BinaryMathOperatorDelegate(string left, string right);
