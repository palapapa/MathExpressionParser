namespace MathExpressionParser
{
    /// <summary>
    /// Parse order: <see cref="Constant"/> > <see cref="Function"/> > <see cref="Exponent"/> > <see cref="NegativeSign"/> > <see cref="MultiplicationDivision"/> > <see cref="AdditionSubtraction"/>.
    /// </summary>
    public enum MathOperatorPrecedence
    {
        AdditionSubtraction,
        MultiplicationDivision,
        NegativeSign,
        Exponent,
        Function,
        Constant
    }
}