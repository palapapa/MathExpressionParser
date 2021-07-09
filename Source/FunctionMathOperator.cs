namespace MEP
{
    public class FunctionMathOperator : MathOperator
    {
        public FunctionMathOperatorDelegate Calculate { get; set; }

        public FunctionMathOperator(string name, MathOperatorPrecedence priority, FunctionMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = priority;
            Calculate = calculate;
        }
    }
}