using System;
using System.Linq;
using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// Represents a mathematical expression.
/// </summary>
public class MathExpression : IMathExpression
{
    private string expression = "";
    /// <summary>
    /// The <see cref="string"/> representation of the math expression this <see cref="MathExpression"/> holds.
    /// </summary>
    /// <exception cref="ArgumentNullException">When this is set to <see langword="null"/>.</exception>
    public string Expression
    {
        get => expression;
        set => expression = value ?? throw new ArgumentNullException(nameof(Expression));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> with <see cref="Expression"/> set to an empty <see cref="string"/>.
    /// </summary>
    public MathExpression()
    {
    }
    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> with <see cref="Expression"/> set to <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">The math expression to use.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is <see langword="null"/>.</exception>
    public MathExpression(string expression)
    {
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> and copies the <see cref="Expression"/> of <paramref name="mathExpression"/> into the new instance.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to copy.</param>
    public MathExpression(MathExpression mathExpression)
    {
        Expression = mathExpression.Expression ?? throw new ArgumentNullException(nameof(mathExpression));
    }
    
    /// <summary>
    /// Tokenizes a math expression into numbers(including scientific notation), operators(including functions), whitespaces, and commas.
    /// </summary>
    /// <returns>A <see cref="List{T}"/> of tokens.</returns>
    /// <exception cref="ArgumentNullException">When the <see name="Expression"/> is null.</exception>
    /// <exception cref="ParserException">When a number in <see name="Expression"/> is of invalid format.</exception>
    private static List<Token> Tokenize(string expression)
    {
        ArgumentNullException.ThrowIfNull(expression, nameof(expression));
        List<Token> tokens = new();
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i].IsDigit())
            {
                int originalI = i;
                for (int j = i; j < expression.Length; j++)
                {
                    if ((!expression[j].IsDigit() && expression[j] is not 'E' and not 'e' and not '+' and not '-' and not '.') ||
                        ((expression[j] is '+' or '-') && expression.BoundElememtAt(j - 1) is not 'E' and not 'e'))
                    {
                        tokens.Add(new(expression[i..j], i));
                        i = j - 1;
                        break;
                    }
                    else if (j == expression.Length - 1)
                    {
                        tokens.Add(new(expression[i..(j + 1)], i));
                        i = j;
                        break;
                    }
                }
                if (!double.TryParse(tokens.Last(), out _))
                {
                    throw new ParserException($"Invalid number format at position {originalI}", new ParserExceptionContext(originalI, ParserExceptionType.InvalidNumberFormat));
                }
            }
            else if (expression[i].IsLetter() || expression[i] is '_')
            {
                for (int j = i; j < expression.Length; j++)
                {
                    if (!expression[j].IsDigit() && !expression[j].IsLetter() && expression[j] != '_')
                    {
                        tokens.Add(new(expression[i..j], i));
                        i = j - 1;
                        break;
                    }
                    else if (j == expression.Length - 1)
                    {
                        tokens.Add(new(expression[i..(j + 1)], i));
                        i = j;
                        break;
                    }
                }
            }
            else if (expression[i].IsWhitespace())
            {
                continue;
            }
            else // symbols
            {
                tokens.Add(new(expression[i].ToString(), i));
            }
        }
        return tokens;
    }

    /// <summary>
    /// Converts this <see cref="MathExpression"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to convert.</param>
    /// <returns><see cref="Expression"/> or <see langword="null"/> if <paramref name="mathExpression"/> is null.</returns>
    public static implicit operator string(MathExpression mathExpression)
    {
        return mathExpression?.ToString();
    }

    /// <summary>
    /// Converts this <see cref="MathExpression"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <returns><see cref="Expression"/>.</returns>
    public override string ToString()
    {
        return Expression;
    }

    /// <summary>
    /// Evaluates the <see cref="Expression"/>.
    /// </summary>
    /// <returns>The value of the <see cref="Expression"/>.</returns>
    public double Evaluate()
    {
        throw new NotImplementedException();
    }
}