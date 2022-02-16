using System;
using System.Linq;
using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// Represents a mathematical expression.
/// </summary>
public class MathExpression : IMathExpression
{
    /// <summary>
    /// The <see cref="string"/> representation of the math expression this <see cref="MathExpression"/> holds.
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> with <see cref="Expression"/> set to <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">The math expression to use.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is <see langword="null"/>.</exception>
    public MathExpression(string expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        Expression = expression;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> and copies the <see cref="Expression"/> of <paramref name="mathExpression"/> into the new instance.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to copy.</param>
    public MathExpression(MathExpression mathExpression)
    {
        Expression = mathExpression.Expression;
    }
    
    /// <summary>
    /// Tokenizes a math expression into numbers(including scientific notation), operators(including functions), whitespaces, and commas.
    /// </summary>
    /// <param name="input">The expression to tokenize.</param>
    /// <returns>A <see cref="List{T}"/> of tokens.</returns>
    /// <exception cref="ArgumentNullException">When the <paramref name="input"/> is null.</exception>
    /// <exception cref="ParserException">When a number in <paramref name="input"/> is of invalid format.</exception>
    private static List<string> Tokenize(string input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        List<string> tokens = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].IsDigit())
            {
                int originalI = i;
                for (int j = i; j < input.Length; j++)
                {
                    if ((!input[j].IsDigit() && input[j] is not 'E' and not 'e' and not '+' and not '-' and not '.') ||
                        ((input[j] is '+' or '-') && input.BoundElememtAt(j - 1) is not 'E' and not 'e'))
                    {
                        tokens.Add(input[i..j]);
                        i = j - 1;
                        break;
                    }
                    else if (j == input.Length - 1)
                    {
                        tokens.Add(input[i..(j + 1)]);
                        i = j;
                        break;
                    }
                }
                try
                {
                    double.Parse(tokens.Last());
                }
                catch (FormatException)
                {
                    throw new ParserException($"Invalid number format at position {originalI}", new ParserExceptionContext(originalI));
                }
            }
            else if (input[i].IsLetter() || input[i] == '_')
            {
                for (int j = i; j < input.Length; j++)
                {
                    if (!input[j].IsDigit() && !input[j].IsLetter() && input[j] != '_')
                    {
                        tokens.Add(input[i..j]);
                        i = j - 1;
                        break;
                    }
                    else if (j == input.Length - 1)
                    {
                        tokens.Add(input[i..(j + 1)]);
                        i = j;
                        break;
                    }
                }
            }
            else // whitespaces or symbols
            {
                tokens.Add(input[i].ToString());
            }
        }
        return tokens;
    }

    /// <summary>
    /// Converts this <see cref="MathExpression"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to convert.</param>
    public static implicit operator string(MathExpression mathExpression)
    {
        return mathExpression.ToString();
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