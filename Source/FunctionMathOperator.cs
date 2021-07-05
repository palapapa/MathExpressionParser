namespace MathExpressionParser
{
    public class FunctionMathOperator : MathOperator
    {
        public FunctionMathOperatorDelegate Calculate { get; set; }

        public FunctionMathOperator(string name, MathOperatorPrecedence priority, FunctionMathOperatorDelegate calculate)
        {
            Name = name;
            Priority = priority;
            Calculate = calculate;
        }
    }
}