namespace MEP;

public class PrefixUnaryMathOperator : UnaryMathOperator
{
    public PrefixUnaryMathOperator(string name, MathOperatorPrecedence precedence, UnaryMathOperatorDelegate calculate) : base(name, precedence, calculate)
    {

    }
}
