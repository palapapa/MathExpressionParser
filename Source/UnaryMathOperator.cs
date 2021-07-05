namespace MathExpressionParser
{
    public class UnaryMathOperator : MathOperator
    {
        public UnaryMathOperatorDelegate Calculate { get; set; }

        public UnaryMathOperator(string name, MathOperatorPrecedence priority, UnaryMathOperatorDelegate calculate)
        {
            Name = name;
            Priority = priority;
            Calculate = calculate;
        }
    }
}