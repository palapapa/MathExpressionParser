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

    private static readonly List<BinaryOperator> builtInBinaryOperators = new()
    {
        new BinaryOperator
        (
            "+",
            OperatorPrecedence.Additive,
            OperatorAssociativity.Left,
            (right, left) => right + left
        ),
        new BinaryOperator
        (
            "-",
            OperatorPrecedence.Additive,
            OperatorAssociativity.Left,
            (right, left) => right - left
        ),
        new BinaryOperator
        (
            "*",
            OperatorPrecedence.Multiplicative,
            OperatorAssociativity.Left,
            (right, left) => right * left
        ),
        new BinaryOperator
        (
            "/",
            OperatorPrecedence.Multiplicative,
            OperatorAssociativity.Left,
            (right, left) => right / left
        ),
        new BinaryOperator
        (
            "^",
            OperatorPrecedence.Exponentiation,
            OperatorAssociativity.Right,
            (right, left) => right.ToThePowerOf(left)
        ),
        new BinaryOperator
        (
            "%",
            OperatorPrecedence.Multiplicative,
            OperatorAssociativity.Left,
            (right, left) => right % left
        )
    };

    private static readonly List<ConstantOperator> builtInConstantOperators = new()
    {
        new ConstantOperator
        (
            "pi",
            Math.PI
        ),
        new ConstantOperator
        (
            "e",
            Math.E
        )
    };

    private static readonly List<FunctionalOperator> builtInFunctionalOperators = new()
    {
        new FunctionalOperator
        (
            "sqrt",
            arguments => Math.Sqrt(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "sin",
            arguments => Math.Sin(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "asin",
            arguments => Math.Asin(arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "cos",
            arguments => Math.Cos(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acos",
            arguments => Math.Acos(arguments.First()).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "tan",
            arguments => Math.Tan(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "atan",
            arguments => Math.Atan(arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "csc",
            arguments => 1 / Math.Sin(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acsc",
            arguments => Math.Asin(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "sec",
            arguments => 1 / Math.Cos(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "asec",
            arguments => Math.Acos(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "cot",
            arguments => 1 / Math.Tan(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acot",
            arguments => Math.Atan(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "sinh",
            arguments => Math.Sinh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "asinh",
            arguments => Math.Asinh(arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "cosh",
            arguments => Math.Cosh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acosh",
            arguments => Math.Acosh(arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "tanh",
            arguments => Math.Tanh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "atanh",
            arguments => Math.Atanh(arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "csch",
            arguments => 1 / Math.Sinh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acsch",
            arguments => Math.Asinh(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "sech",
            arguments => 1 / Math.Cosh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "asech",
            arguments => Math.Acosh(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "coth",
            arguments => 1 / Math.Tanh(arguments[0].Deg2Rad()),
            1
        ),
        new FunctionalOperator
        (
            "acoth",
            arguments => Math.Atanh(1 / arguments[0]).Rad2Deg(),
            1
        ),
        new FunctionalOperator
        (
            "P",
            arguments =>
            {
                long n = (long)arguments[0];
                long r = (long)arguments[1];
                return Utilities.Permutation(n, r);
            },
            2
        ),
        new FunctionalOperator
        (
            "C",
            arguments =>
            {
                long n = (long)arguments[0];
                long r = (long)arguments[1];
                return Utilities.Combination(n, r);
            },
            2
        ),
        new FunctionalOperator
        (
            "H",
            arguments =>
            {
                long n = (long)arguments[0];
                long r = (long)arguments[1];
                return Utilities.Combination(n + r - 1, r);
            },
            2
        ),
        new FunctionalOperator
        (
            "log",
            arguments => Utilities.Log(arguments[0], arguments[1]),
            2
        ),
        new FunctionalOperator
        (
            "log10",
            arguments => Math.Log10(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "log2",
            arguments => Math.Log2(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "ln",
            arguments => Math.Log(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "ceil",
            arguments => Math.Ceiling(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "floor",
            arguments => Math.Floor(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "round",
            arguments => Math.Round(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "abs",
            arguments => Math.Abs(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "min",
            arguments => arguments.Min()
        ),
        new FunctionalOperator
        (
            "max",
            arguments => arguments.Max()
        )
    };

    private static readonly List<PrefixUnaryOperator> builtInPrefixUnaryOperator = new()
    {
        new PrefixUnaryOperator
        (
            "-",
            OperatorPrecedence.Unary,
            input => -input
        )
    };

    private static readonly List<PostfixUnaryOperator> builtInPostfixUnaryOperators = new()
    {
        new PostfixUnaryOperator
        (
            "!",
            OperatorPrecedence.Exponentiation,
            input => ((long)input).Factorial()
        ),
        new PostfixUnaryOperator
        (
            "torad",
            OperatorPrecedence.Exponentiation,
            input => input.Deg2Rad()
        ),
        new PostfixUnaryOperator
        (
            "todeg",
            OperatorPrecedence.Exponentiation,
            input => input.Rad2Deg()
        )
    };

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
