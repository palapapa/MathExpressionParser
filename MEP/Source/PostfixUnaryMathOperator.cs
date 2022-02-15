namespace MEP;

public class PostfixUnaryMathOperator : UnaryMathOperator
{
    public PostfixUnaryMathOperator(string name, MathOperatorPrecedence precedence, UnaryMathOperatorDelegate calculate) : base(name, precedence, calculate)
    {

    }
}
