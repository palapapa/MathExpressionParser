namespace MEP
{
    public class UnaryMathOperator : MathOperator
    {
        public UnaryMathOperatorDelegate Calculate { get; set; }

        public UnaryMathOperator(string name, MathOperatorPrecedence precedence, UnaryMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = precedence;
            Calculate = calculate;
        }
    }
}