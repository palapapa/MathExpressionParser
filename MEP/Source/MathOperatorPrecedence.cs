namespace MEP;

/// <summary>
/// Represents how early or late should a <see cref="MathOperator"/> be parsed in a expression.<br/>
/// </summary>
/// <remarks>
/// Parse order(smaller number first): 
/// <list type="number">
/// <item><see cref="Constant"/></item>
/// <item><see cref="Function"/></item>
/// <item><see cref="Exponentiation"/></item>
/// <item><see cref="Unary"/></item>
/// <item><see cref="MultiplicationDivision"/></item>
/// <item><see cref="AdditionSubtraction"/></item>
/// </list>
/// The operands of a <see cref="MathOperator"/> must have a lower precedence than the <see cref="MathOperator"/> itself.
/// </remarks>
public enum MathOperatorPrecedence
{
    AdditionSubtraction,
    MultiplicationDivision,
    Unary,
    Exponentiation,
    Function,
    Constant
}
