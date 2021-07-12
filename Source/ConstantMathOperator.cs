namespace MEP
{
    public class ConstantMathOperator : MathOperator
    {
        public string Value { get; set; }

        public ConstantMathOperator(string name, MathOperatorPrecedence precedence, string value)
        {
            Name = name;
            Precedence = precedence;
            Value = value;
        }
    }
}