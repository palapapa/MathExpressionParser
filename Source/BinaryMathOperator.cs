namespace MEP
{
    public class BinaryMathOperator : MathOperator
    {
        public BinaryMathOperatorDelegate Calculate { get; set; }

        public BinaryMathOperator(string name, MathOperatorPrecedence precedence, BinaryMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = precedence;
            Calculate = calculate;
        }
    }
}