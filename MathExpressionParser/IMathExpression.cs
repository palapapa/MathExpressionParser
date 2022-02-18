namespace MathExpressionParser;

/// <summary>
/// Exposes common properties and methods of mathematical expressions.
/// </summary>
public interface IMathExpression
{
    /// <summary>
    /// Represents the math expression.
    /// </summary>
    public string Expression { get; set; }

    /// <summary>
    /// Evaluates the <see cref="Expression"/> and returns a value.
    /// </summary>
    /// <returns>The value of <see cref="Expression"/>.</returns>
    public double Evaluate();
}
