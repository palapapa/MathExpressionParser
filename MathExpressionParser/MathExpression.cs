using System;
using System.Linq;
using System.Collections.Generic;

namespace MathExpressionParser;

/// <summary>
/// Represents a mathematical expression.
/// </summary>
public class MathExpression : IMathExpression, IComparable<MathExpression>, IEquatable<MathExpression>
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

    private List<Token> cachedTokens = new();

    private IList<FunctionalOperator> customFunctions = new List<FunctionalOperator>();

    /// <summary>
    /// Any <see cref="FunctionalOperator"/> in this <see cref="IList{T}"/> will be used when <see cref="Evaluate"/> is called.
    /// </summary>
    public IList<FunctionalOperator> CustomFunctions
    {
        get => customFunctions;
        set => customFunctions = value ?? throw new ArgumentNullException(nameof(CustomFunctions));
    }

    private IList<ConstantOperator> customConstants = new List<ConstantOperator>();

    /// <summary>
    /// Any <see cref="ConstantOperator"/> in this <see cref="IList{T}"/> will be used when <see cref="Evaluate"/> is called.
    /// </summary>
    public IList<ConstantOperator> CustomConstants
    {
        get => customConstants;
        set => customConstants = value ?? throw new ArgumentNullException(nameof(CustomConstants));
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
            arguments => Math.Sin(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "asin",
            arguments => Math.Asin(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "cos",
            arguments => Math.Cos(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acos",
            arguments => Math.Acos(arguments.First()),
            1
        ),
        new FunctionalOperator
        (
            "tan",
            arguments => Math.Tan(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "atan",
            arguments => Math.Atan(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "csc",
            arguments => 1 / Math.Sin(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acsc",
            arguments => Math.Asin(1 / arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "sec",
            arguments => 1 / Math.Cos(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "asec",
            arguments => Math.Acos(1 / arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "cot",
            arguments => 1 / Math.Tan(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acot",
            arguments => Math.Atan(1 / arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "sinh",
            arguments => Math.Sinh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "asinh",
            arguments => Math.Asinh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "cosh",
            arguments => Math.Cosh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acosh",
            arguments => Math.Acosh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "tanh",
            arguments => Math.Tanh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "atanh",
            arguments => Math.Atanh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "csch",
            arguments => 1 / Math.Sinh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acsch",
            arguments => Math.Asinh(1 / arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "sech",
            arguments => 1 / Math.Cosh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "asech",
            arguments => Math.Acosh(1 / arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "coth",
            arguments => 1 / Math.Tanh(arguments[0]),
            1
        ),
        new FunctionalOperator
        (
            "acoth",
            arguments => Math.Atanh(1 / arguments[0]),
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

    private static readonly List<PrefixUnaryOperator> builtInPrefixUnaryOperators = new()
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

    private static readonly List<Operator> builtInOperators =
        builtInBinaryOperators.Cast<Operator>()
        .Concat(builtInConstantOperators.Cast<Operator>())
        .Concat(builtInFunctionalOperators.Cast<Operator>())
        .Concat(builtInPostfixUnaryOperators.Cast<Operator>())
        .Concat(builtInPrefixUnaryOperators.Cast<Operator>())
        .ToList();

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
    /// Initializes a new instance of <see cref="MathExpression"/> with <see cref="CustomFunctions"/> set to <paramref name="customFunctions"/>, <see cref="CustomConstants"/> set to <paramref name="customConstants"/>, and <see cref="Expression"/> set to an empty <see cref="string"/>. <br/>
    /// If <paramref name="customFunctions"/> or <paramref name="customConstants"/> is <see langword="null"/>, it will remain as an empty <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="customFunctions">The <see cref="IList{T}"/> to set <see cref="CustomFunctions"/> to, or <see langword="null"/> if you wish to leave it empty.</param>
    /// <param name="customConstants">The <see cref="IList{T}"/> to set <see cref="CustomConstants"/> to, or <see langword="null"/> if you wish to leave it empty.</param>
    public MathExpression(IList<FunctionalOperator> customFunctions, IList<ConstantOperator> customConstants)
    {
        CustomFunctions = customFunctions ?? new List<FunctionalOperator>();
        CustomConstants = customConstants ?? new List<ConstantOperator>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> with <see cref="Expression"/> set to <paramref name="expression"/>, <see cref="CustomFunctions"/> set to <paramref name="customFunctions"/> and <see cref="CustomConstants"/> set to <paramref name="customConstants"/>. <br/>
    /// If <paramref name="customFunctions"/> or <paramref name="customConstants"/> is <see langword="null"/>, it will remain as an empty <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="expression"><inheritdoc cref="MathExpression(string)" path="/param[@name='expression']"/></param>
    /// <param name="customFunctions"><inheritdoc cref="MathExpression(IList{FunctionalOperator}, IList{ConstantOperator})" path="/param[@name='customFunctions']"/></param>
    /// <param name="customConstants"><inheritdoc cref="MathExpression(IList{FunctionalOperator}, IList{ConstantOperator})" path="/param[@name='customConstants']"/></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MathExpression(string expression, IList<FunctionalOperator> customFunctions, IList<ConstantOperator> customConstants)
    {
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        CustomFunctions = customFunctions ?? new List<FunctionalOperator>();
        CustomConstants = customConstants ?? new List<ConstantOperator>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MathExpression"/> and copies each member of <paramref name="mathExpression"/> into the new instance.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to copy.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="mathExpression"/> is <see langword="null"/>.</exception>
    public MathExpression(MathExpression mathExpression)
    {
        ArgumentNullException.ThrowIfNull(mathExpression);
        Expression = mathExpression.Expression;
        CustomFunctions = mathExpression.CustomFunctions?.ToList();
        CustomConstants = mathExpression.CustomConstants?.ToList();
    }
    
    private ParserException TryTokenize(out List<Token> result)
    {
        ArgumentNullException.ThrowIfNull(Expression, nameof(Expression));
        List<Token> tokens = new();
        result = null;
        for (int i = 0; i < Expression.Length; i++)
        {
            if (Expression[i].IsDigit())
            {
                int originalI = i;
                string token = "";
                for (int j = i; j < Expression.Length; j++)
                {
                    if ((!Expression[j].IsDigit() && Expression[j] is not 'E' and not 'e' and not '+' and not '-' and not '.') ||
                        ((Expression[j] is '+' or '-') && Expression.BoundElememtAt(j - 1) is not 'E' and not 'e'))
                    {
                        token = Expression[i..j];
                        tokens.Add(new(token, i));
                        i = j - 1;
                        break;
                    }
                    else if (j == Expression.Length - 1)
                    {
                        token = Expression[i..(j + 1)];
                        tokens.Add(new(token, i));
                        i = j;
                        break;
                    }
                }
                if (!double.TryParse(tokens.Last().ToString(), out double value))
                {
                    return new ParserException($"Invalid number format at position {originalI}", new ParserExceptionContext(originalI, ParserExceptionType.InvalidNumberFormat));
                }
                else
                {
                    tokens[^1] = new NumberToken(token, originalI, value);
                }
            }
            else if (Expression[i].IsLetter() || Expression[i] is '_')
            {
                for (int j = i; j < Expression.Length; j++)
                {
                    if (!Expression[j].IsDigit() && !Expression[j].IsLetter() && Expression[j] != '_')
                    {
                        tokens.Add(new(Expression[i..j], i));
                        i = j - 1;
                        break;
                    }
                    else if (j == Expression.Length - 1)
                    {
                        tokens.Add(new(Expression[i..(j + 1)], i));
                        i = j;
                        break;
                    }
                }
            }
            else if (Expression[i].IsWhitespace())
            {
                continue;
            }
            else // symbols
            {
                tokens.Add(new(Expression[i].ToString(), i));
            }
        }
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].ToString() == "-" && (i == 0 || tokens[i - 1] is BinaryOperatorToken or OpeningParenthesisToken or CommaToken or PrefixUnaryOperatorToken))
            {
                tokens[i] = new PrefixUnaryOperatorToken("-", tokens[i].Position);
            }
            else if (builtInBinaryOperators.Any(o => o.Name == tokens[i].ToString()))
            {
                tokens[i] = new BinaryOperatorToken(tokens[i].ToString(), tokens[i].Position);
            }
            else if (builtInConstantOperators.Concat(CustomConstants).Any(o => o.Name == tokens[i].ToString()))
            {
                tokens[i] = new ConstantOperatorToken(tokens[i].ToString(), tokens[i].Position);
            }
            else if (builtInFunctionalOperators.Concat(CustomFunctions).Any(o => o.Name == tokens[i].ToString()))
            {
                tokens[i] = new FunctionalOperatorToken(tokens[i].ToString(), tokens[i].Position, -1);
            }
            else if (builtInPostfixUnaryOperators.Any(o => o.Name == tokens[i].ToString()))
            {
                tokens[i] = new PostfixUnaryOperatorToken(tokens[i].ToString(), tokens[i].Position);
            }
            else if (builtInPrefixUnaryOperators.Any(o => o.Name == tokens[i].ToString()))
            {
                tokens[i] = new PrefixUnaryOperatorToken(tokens[i].ToString(), tokens[i].Position);
            }
            else if (tokens[i].ToString() == "(")
            {
                tokens[i] = new OpeningParenthesisToken(tokens[i].Position);
            }
            else if (tokens[i].ToString() == ")")
            {
                tokens[i] = new ClosingParenthesisToken(tokens[i].Position);
            }
            else if (tokens[i].ToString() == ",")
            {
                tokens[i] = new CommaToken(tokens[i].Position);
            }
            else
            {
                if (tokens[i] is not NumberToken)
                {
                    return new ParserException($"\"{tokens[i]}\" is not a known operator", new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnknownOperator));
                }
            }
        }
        result = tokens;
        return null;
    }

    /// <summary>
    /// Returns the same <see cref="List{T}"/> as <paramref name="tokens"/> but with every <see cref="FunctionalOperatorToken.ArgumentCount"/> set to the correct number.
    /// This method assumes the provided <paramref name="tokens"/> represent a valid expression.
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    private static List<Token> GetArgumentCount(List<Token> tokens)
    {
        List<Token> result = tokens.ToList();
        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] is FunctionalOperatorToken f)
            {
                int argumentCount = 1;
                for (int j = i + 2; j < result.Count; j++)
                {
                    if (result[j] is CommaToken)
                    {
                        argumentCount++;
                    }
                    else if (result[j] is OpeningParenthesisToken)
                    {
                        int opening = 1;
                        for (int k = j + 1; k < result.Count; k++) // skips to the corresponding closing parenthesis
                        {
                            if (tokens[k] is OpeningParenthesisToken)
                            {
                                opening++;
                            }
                            else if (tokens[k] is ClosingParenthesisToken)
                            {
                                opening--;
                            }
                            if (opening is 0)
                            {
                                j = k;
                                break;
                            }
                        }
                    }
                    else if (result[j] is ClosingParenthesisToken)
                    {
                        if (tokens[j - 1] is OpeningParenthesisToken)
                        {
                            argumentCount = 0;
                        }
                        break;
                    }
                }
                result[i] = f with { ArgumentCount = argumentCount };
            }
        }
        return result;
    }

    /// <summary>
    /// Converts this <see cref="MathExpression"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="mathExpression">The <see cref="MathExpression"/> to convert.</param>
    /// <returns><see cref="Expression"/> or <see langword="null"/> if <paramref name="mathExpression"/> is null.</returns>
    public static explicit operator string(MathExpression mathExpression)
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
    /// Checks if <see cref="Expression"/> is a valid math expression.
    /// </summary>
    /// <returns>A <see cref="ParserException"/> instance that contains information about the error, or <see langword="null"/> if the expression is valid.</returns>
    public ParserException Validate()
    {
        foreach (FunctionalOperator f in CustomFunctions)
        {
            if (f is null)
            {
                return new($"{nameof(CustomFunctions)} contains a null element",
                    new ParserExceptionContext(-1, ParserExceptionType.NullCustomFunction));
            }
            if (f.Name.Length is 0 || f.Name[0].IsDigit() || f.Name.Any(c => !c.IsDigit() && !c.IsLetter() && c is not '_'))
            {
                return new($"\"{f.Name}\" is not a valid function name",
                    new ParserExceptionContext(-1, ParserExceptionType.InvalidCustomFunctionName));
            }
        }
        foreach (ConstantOperator c in CustomConstants)
        {
            if (c is null)
            {
                return new($"{nameof(CustomConstants)} contains a null element",
                    new ParserExceptionContext(-1, ParserExceptionType.NullCustomConstant));
            }
            if (c.Value is double.NaN)
            {
                return new($"Constant \"{c.Name}\" have a value of NaN",
                    new ParserExceptionContext(-1, ParserExceptionType.NaNConstant));
            }
            if (c.Name.Length is 0 || c.Name[0].IsDigit() || c.Name.Any(c => !c.IsDigit() && !c.IsLetter() && c is not '_'))
            {
                return new($"\"{c.Name}\" is not a valid constant name",
                    new ParserExceptionContext(-1, ParserExceptionType.InvalidCustomConstantName));
            }
        }
        List<string> conflictingOperators = CustomFunctions.Cast<Operator>()
            .Concat(CustomConstants.Cast<Operator>())
            .Concat(builtInFunctionalOperators.Cast<Operator>())
            .Concat(builtInConstantOperators.Cast<Operator>())
            .GroupBy(o => o.Name)
            .Where(grouping => grouping.Count() > 1)
            .Select(grouping => grouping.Key)
            .ToList();
        if (conflictingOperators.Count > 0)
        {
            return new($"Operators \"{string.Join(", ", conflictingOperators)}\" have multiple definitions",
                new ParserExceptionContext(-1, ParserExceptionType.ConflictingNames));
        }
        Stack<ParserState> states = new();
        states.Push(ParserState.Start);
        ParserException error = TryTokenize(out List<Token> tokens);
        if (error is not null)
        {
            return error;
        }
        if (tokens.Count is 0)
        {
            return null;
        }
        for (int i = 0; i < tokens.Count; i++)
        {
            ParserState currentState = states.Peek();
            if (tokens[i] is BinaryOperatorToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingBinaryOperator))
                {
                    states.Pop();
                    states.Push(ParserState.Start);
                }
                else
                {
                    return new($"Unexpected binary operator \"{tokens[i]}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedBinaryOperator));
                }
            }
            else if (tokens[i] is ClosingParenthesisToken)
            {
                if ((currentState.HasFlag(ParserState.ExpectingClosingParenthesis) &&
                    states.Skip(1).FirstOrDefault().HasFlag(ParserState.ExpectingClosingParenthesis)) ||
                    (states.Skip(1).FirstOrDefault().HasFlag(ParserState.InFunction) &&
                    tokens[i - 1] is OpeningParenthesisToken)) // the conditions after "||" take care of the case where a function has no arguments
                {
                    states.Pop();
                    states.Pop();
                    states.Push(ParserState.AfterClosingParenthesis);
                }
                else
                {
                    return new($"Unexpected closing parenthesis at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedClosingParenthesis));
                }
            }
            else if (tokens[i] is CommaToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingComma) &&
                    states.Skip(1).FirstOrDefault().HasFlag(ParserState.InFunction))
                {
                    states.Pop();
                    states.Push(ParserState.Start);
                }
                else
                {
                    return new($"Unexpected comma at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedComma));
                }
            }
            else if (tokens[i] is ConstantOperatorToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingConstant))
                {
                    states.Pop();
                    states.Push(ParserState.AfterNumber);
                }
                else
                {
                    return new($"Unexpected constant \"{tokens[i]}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedConstantOperator));
                }
            }
            else if (tokens[i] is FunctionalOperatorToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingFunctionalOperator))
                {
                    states.Pop();
                    states.Push(ParserState.ExpectingOpeningParenthesis | ParserState.InFunction);
                }
                else
                {
                    return new($"Unexpected function \"{tokens[i].Position}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedFunctionalOperator));
                }
            }
            else if (tokens[i] is NumberToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingNumber))
                {
                    states.Pop();
                    states.Push(ParserState.AfterNumber);
                }
                else
                {
                    return new($"Unexpected number \"{tokens[i]}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedNumber));
                }
            }
            else if (tokens[i] is OpeningParenthesisToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingOpeningParenthesis))
                {
                    ParserState state = states.Pop();
                    if (state.HasFlag(ParserState.InFunction))
                    {
                        states.Push(ParserState.ExpectingClosingParenthesis | ParserState.InFunction);
                    }
                    else
                    {
                        states.Push(ParserState.ExpectingClosingParenthesis);
                    }
                    states.Push(ParserState.Start);
                }
                else
                {
                    return new($"Unexpected opening parenthesis at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedOpeningParenthesis));
                }
            }
            else if (tokens[i] is PostfixUnaryOperatorToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingPostfixUnaryOperator))
                {
                    states.Pop();
                    states.Push(ParserState.AfterPostfix);
                }
                else
                {
                    return new($"Unexpected postfix unary operator \"{tokens[i]}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedPostfixUnaryOperator));
                }
            }
            else if (tokens[i] is PrefixUnaryOperatorToken)
            {
                if (currentState.HasFlag(ParserState.ExpectingPrefixUnaryOperator))
                {
                    states.Pop();
                    states.Push(ParserState.ExpectingOperand);
                }
                else
                {
                    return new($"Unexpected prefix unary operator \"{tokens[i]}\" at position {tokens[i].Position}",
                        new ParserExceptionContext(tokens[i].Position, ParserExceptionType.UnexpectedPrefixUnaryOperator));
                }
            }
        }
        if (tokens[^1] is BinaryOperatorToken or CommaToken or FunctionalOperatorToken or OpeningParenthesisToken or PrefixUnaryOperatorToken)
        {
            return new($"Expression ended unexpectedly",
                new ParserExceptionContext(Expression.Length, ParserExceptionType.UnexpectedNewline));
        }
        if (states.Count != 1)
        {
            return new($"One or more closing parentheses are missing",
                new ParserExceptionContext(Expression.Length, ParserExceptionType.TooManyOpeningParentheses));
        }
        // At this point, everything other than argument counts has been validated.
        tokens = GetArgumentCount(tokens);
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] is FunctionalOperatorToken f)
            {
                FunctionalOperator correspondingFunction = builtInFunctionalOperators.FirstOrDefault(function => function.Name == f.Content) ?? CustomFunctions.First(function => function.Name == f.Content);
                if (correspondingFunction.ArgumentCounts.Count is not 0 && !correspondingFunction.ArgumentCounts.Contains(f.ArgumentCount))
                {
                    return new($"Function \"{f.Content}\" at position {f.Position} has {f.ArgumentCount} arguments, but the function expected {string.Join(", ", correspondingFunction.ArgumentCounts)} arguments",
                        new ParserExceptionContext(f.Position, ParserExceptionType.IncorrectArgumentCount));
                }
            }
        }
        cachedTokens = tokens;
        return null;
    }

    /// <summary>
    /// Tries to evaluate <see cref="Expression"/>. This can still throw an exception if any <see cref="FunctionalOperator"/> used throws an exception itself.
    /// </summary>
    /// <param name="result">The result of the evaluation if it succeeds.</param>
    /// <returns>
    /// A <see cref="ParserException"/> instance containing information about the error if <paramref name="result"/> is not a valid math expression, or <see langword="null"/> if it is. <br/>
    /// If a non-<see langword="null"/> <see cref="ParserException"/> is returned, <paramref name="result"/> will be set to <see cref="double.NaN"/>. <br/>
    /// If <see cref="Expression"/> is whitespace or empty, <paramref name="result"/> will be set to <see cref="double.NaN"/>, <br/>
    /// Otherwise, <paramref name="result"/> will be set to the result of the evaluation.
    /// </returns>
    public ParserException TryEvaluate(out double result)
    {
        result = double.NaN;
        if (Expression.IsWhitespace())
        {
            return null;
        }
        ParserException error = Validate();
        if (error is not null)
        {
            return error;
        }
        List<Token> tokens = cachedTokens;
        Stack<Token> output = new(), operators = new();
        Dictionary<Token, Operator> tokenToOperator = new();
        foreach (Token token in tokens)
        {
            if (token is BinaryOperatorToken)
            {
                tokenToOperator.Add(token, builtInBinaryOperators.First(o => o.Name == token.Content));
            }
            else if (token is ConstantOperatorToken)
            {
                tokenToOperator.Add(token, builtInConstantOperators.Concat(CustomConstants).First(o => o.Name == token.Content));
            }
            else if (token is FunctionalOperatorToken)
            {
                tokenToOperator.Add(token, builtInFunctionalOperators.Concat(CustomFunctions).First(o => o.Name == token.Content));
            }
            else if (token is PostfixUnaryOperatorToken)
            {
                tokenToOperator.Add(token, builtInPostfixUnaryOperators.First(o => o.Name == token.Content));
            }
            else if (token is PrefixUnaryOperatorToken)
            {
                tokenToOperator.Add(token, builtInPrefixUnaryOperators.First(o => o.Name == token.Content));
            }
        }
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] is BinaryOperatorToken or PostfixUnaryOperatorToken or PrefixUnaryOperatorToken)
            {
                /*
                 while (there is an operator o2 other than the left parenthesis at the top of the operator stack &&
                     ((o2 has greater precedence than o1) || (they have the same precedence && o1 is left-associative)))
                 {
                     pop o2 from the operator stack into the output queue
                 }
                */
                Token top = null;
                IOperatorLike o1 = (IOperatorLike)tokenToOperator[tokens[i]];
                while (operators.TryPeek(out top) && top is BinaryOperatorToken or PostfixUnaryOperatorToken or PrefixUnaryOperatorToken)
                {
                    IOperatorLike o2 = (IOperatorLike)tokenToOperator[top];
                    if (o1 is not UnaryOperator &&
                        (((int)o2.Precedence > (int)o1.Precedence) || (o2.Precedence == o1.Precedence && o1.Associativity is OperatorAssociativity.Left)))
                    {
                        output.Push(operators.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                operators.Push(tokens[i]);
            }
            else if (tokens[i] is ClosingParenthesisToken)
            {
                while (operators.Peek() is not OpeningParenthesisToken)
                {
                    output.Push(operators.Pop());
                }
                operators.Pop();
                if (operators.TryPeek(out Token top) && top is FunctionalOperatorToken)
                {
                    output.Push(operators.Pop());
                }
            }
            else if (tokens[i] is CommaToken)
            {
                continue;
            }
            else if (tokens[i] is ConstantOperatorToken)
            {
                output.Push(tokens[i]);
            }
            else if (tokens[i] is FunctionalOperatorToken)
            {
                operators.Push(tokens[i]);
            }
            else if (tokens[i] is NumberToken)
            {
                output.Push(tokens[i]);
            }
            else if (tokens[i] is OpeningParenthesisToken)
            {
                operators.Push(tokens[i]);
            }
        }
        int operatorsCount = operators.Count;
        for (int i = 0; i < operatorsCount; i++)
        {
            output.Push(operators.Pop());
        }
        List<Token> postfixTokens = output.Reverse().ToList();
        Stack<double> operands = new();
        for (int i = 0; i < postfixTokens.Count; i++)
        {
            if (postfixTokens[i] is NumberToken num)
            {
                operands.Push(num.Value);
            }
            else if (postfixTokens[i] is ConstantOperatorToken)
            {
                operands.Push(((ConstantOperator)tokenToOperator[postfixTokens[i]]).Value);
            }
            else if (postfixTokens[i] is FunctionalOperatorToken functionToken)
            {
                FunctionalOperator function = (FunctionalOperator)tokenToOperator[postfixTokens[i]];
                List<double> arguments = new();
                for (int j = 0; j < functionToken.ArgumentCount; j++)
                {
                    arguments.Add(operands.Pop());
                }
                arguments.Reverse();
                operands.Push(function.Calculate(arguments));
            }
            else if (postfixTokens[i] is BinaryOperatorToken)
            {
                BinaryOperator binaryOperator = (BinaryOperator)tokenToOperator[postfixTokens[i]];
                double right = operands.Pop(), left = operands.Pop();
                operands.Push(binaryOperator.Calculate(left, right));
            }
            else if (postfixTokens[i] is PostfixUnaryOperatorToken)
            {
                PostfixUnaryOperator postfixUnaryOperator = (PostfixUnaryOperator)tokenToOperator[postfixTokens[i]];
                operands.Push(postfixUnaryOperator.Calculate(operands.Pop()));
            }
            else if (postfixTokens[i] is PrefixUnaryOperatorToken)
            {
                PrefixUnaryOperator prefixUnaryOperator = (PrefixUnaryOperator)tokenToOperator[postfixTokens[i]];
                operands.Push(prefixUnaryOperator.Calculate(operands.Pop()));
            }
        }
        result = operands.Pop();
        return null;
    }

    /// <summary>
    /// Evaluates <see cref="Expression"/>.
    /// </summary>
    /// <returns>The value of the <see cref="Expression"/>.</returns>
    /// <exception cref="ParserException">If <see cref="Expression"/> is not a valid math expression.</exception>
    public double Evaluate()
    {
        ParserException error = TryEvaluate(out double result);
        if (error is null)
        {
            return result;
        }
        else
        {
            throw error;
        }
    }

    /// <summary>
    /// Compares the value of <see cref="Evaluate"/> of this instance to that of <paramref name="other"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>
    /// <list type="bullet">
    /// <item>> 0 if the value of <see cref="Evaluate"/> of this instance is larger than that of <paramref name="other"/>.</item>
    /// <item>= 0 if the value of <see cref="Evaluate"/> of this instance is the same as that of <paramref name="other"/>.</item>
    /// <item>&lt; 0 if the value of <see cref="Evaluate"/> of this instance is smaller than that of <paramref name="other"/>.</item>
    /// </list>
    /// </returns>
    /// <exception cref="ParserException">If either this instance or <paramref name="other"/> throws <see cref="ParserException"/> on calling its <see cref="Evaluate"/>.</exception>
    public int CompareTo(MathExpression other)
    {
        if (other is null)
        {
            return 1;
        }
        return Evaluate().CompareTo(other.Evaluate());
    }

    /// <summary>
    /// Check if the value of <see cref="Evaluate"/> of this instance is equal to that of <paramref name="other"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ParserException"><inheritdoc cref="CompareTo(MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public bool Equals(MathExpression other)
    {
        return Evaluate() == other.Evaluate();
    }

    /// <summary>
    /// <inheritdoc cref="Equals(MathExpression)" path="/summary"/>
    /// </summary>
    /// <param name="obj">A <see cref="double"/> or a <see cref="IMathExpression"/>.</param>
    /// <returns> If <paramref name="obj"/> is not a <see cref="double"/> or an <see cref="IMathExpression"/>, this always returns <see langword="false"/>.</returns>
    public override bool Equals(object obj)
    {
        return (obj is double val && val == Evaluate()) || (obj is IMathExpression expression && expression.Evaluate() == Evaluate());
    }

    /// <summary>
    /// <inheritdoc cref="object.GetHashCode" path="/summary"/>
    /// </summary>
    /// <returns><inheritdoc cref="object.GetHashCode" path="/returns"/></returns>
    /// <exception cref="ParserException">If this instance throws <see cref="ParserException"/> on calling its <see cref="Evaluate"/>.</exception>
    public override int GetHashCode()
    {
        return Evaluate().GetHashCode();
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is smaller than that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is smaller than that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator <(MathExpression left, MathExpression right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is smaller than or equal to that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is smaller or equal to than that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator <=(MathExpression left, MathExpression right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is larger than that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is larger than that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator >(MathExpression left, MathExpression right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is larger than or equal to that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is larger than or equal to that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator >=(MathExpression left, MathExpression right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is equal to that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is equal to that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator ==(MathExpression left, MathExpression right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    /// <summary>
    /// Checks if the value of <see cref="Evaluate"/> of <paramref name="left"/> is not equal to that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns><see langword="true"/> if the value of <see cref="Evaluate"/> of <paramref name="left"/> is not equal to that of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static bool operator !=(MathExpression left, MathExpression right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Converts <paramref name="mathExpression"/> to a <see cref="double"/> with the value of <see cref="Evaluate"/> of <paramref name="mathExpression"/>.
    /// </summary>
    /// <param name="mathExpression"></param>
    /// <returns>Value of <see cref="Evaluate"/> of <paramref name="mathExpression"/>.</returns>
    /// <exception cref="ParserException">If <paramref name="mathExpression"/> throws <see cref="ParserException"/> on calling its <see cref="Evaluate"/>.</exception>
    public static explicit operator double(MathExpression mathExpression)
    {
        return mathExpression.Evaluate();
    }

    /// <summary>
    /// Adds the value of <see cref="Evaluate"/> of the two <see cref="MathExpression"/> together.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ParserException">When either of the arguments throws <see cref="ParserException"/> on calling its <see cref="Evaluate"/>.</exception>
    public static double operator +(MathExpression left, MathExpression right)
    {
        return left.Evaluate() + right.Evaluate();
    }

    /// <summary>
    /// Substracts the value of <see cref="Evaluate"/> of <paramref name="right"/> from that of <paramref name="left"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static double operator -(MathExpression left, MathExpression right)
    {
        return left.Evaluate() - right.Evaluate();
    }

    /// <summary>
    /// Multiplies the value of <see cref="Evaluate"/> of the two <see cref="MathExpression"/> together.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static double operator *(MathExpression left, MathExpression right)
    {
        return left.Evaluate() * right.Evaluate();
    }

    /// <summary>
    /// Divide the value of <see cref="Evaluate"/> of <paramref name="left"/> by that of <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ParserException"><inheritdoc cref="operator +(MathExpression, MathExpression)" path="/exception[@cref='ParserException']"/></exception>
    public static double operator /(MathExpression left, MathExpression right)
    {
        return left.Evaluate() / right.Evaluate();
    }
}
