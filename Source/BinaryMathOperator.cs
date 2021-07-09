namespace MEP
{
    public class BinaryMathOperator : MathOperator
    {
        public BinaryMathOperatorDelegate Calculate { get; set; }

        public BinaryMathOperator(string name, MathOperatorPrecedence priority, BinaryMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = priority;
            Calculate = calculate;
        }
    }
}