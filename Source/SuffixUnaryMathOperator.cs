namespace MEP
{
    public class SuffixUnaryMathOperator : UnaryMathOperator
    {
        public SuffixUnaryMathOperator(string name, MathOperatorPrecedence precedence, UnaryMathOperatorDelegate calculate) : base(name, precedence, calculate)
        {

        }
    }
}