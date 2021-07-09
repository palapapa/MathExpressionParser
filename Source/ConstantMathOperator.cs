namespace MEP
{
    public class ConstantMathOperator : MathOperator
    {
        public string Value { get; set; }

        public ConstantMathOperator(string name, MathOperatorPrecedence priority, string value)
        {
            Name = name;
            Precedence = priority;
            Value = value;
        }
    }
}