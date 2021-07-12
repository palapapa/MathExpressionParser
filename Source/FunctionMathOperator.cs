namespace MEP
{
    public class FunctionMathOperator : MathOperator
    {
        public FunctionMathOperatorDelegate Calculate { get; set; }

        public FunctionMathOperator(string name, MathOperatorPrecedence precedence, FunctionMathOperatorDelegate calculate)
        {
            Name = name;
            Precedence = precedence;
            Calculate = calculate;
        }
    }
}