namespace MEP
{
    public class UnaryMathOperator : MathOperator
    {
        public UnaryMathOperatorDelegate Calculate { get; set; }

        public UnaryMathOperator(string name, MathOperatorPrecedence priority, UnaryMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = priority;
            Calculate = calculate;
        }
    }
}