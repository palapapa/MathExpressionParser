namespace MEP;

/// <summary>
/// Represents a operator that takes the left and right tokens as its operands.
/// </summary>
public class BinaryMathOperator : MathOperator
{
    /// <summary>
    /// The delegate used to compute the result of this operator.
    /// </summary>
    public BinaryMathOperatorDelegate Calculate { get; set; }

    public BinaryMathOperator(string name, MathOperatorPrecedence precedence, BinaryMathOperatorDelegate calculate)
    {
        Name = name;
        Precedence = precedence;
        Calculate = calculate;
    }
}
