using System;
using System.Collections.Generic;
using System.Linq;

namespace MEP;

public class MathOperator
{
    /// <summary>
    /// The <see cref="string"/> representation of the <see cref="MathOperator"/> in a math expression. <br/>
    /// </summary>
    /// <remarks>
    /// It can only contain letters, numbers, underscores and symbols(excluding '(', ')', ',' and '.'). It cannot start with a number. If it contains a symbol, then it must have a <see cref="string.Length"/> of 1.
    /// </remarks>
    public string Name { get; set; }
    public MathOperatorPrecedence Precedence { get; set; }

    public static IEnumerable<MathOperator> GetDefaultOperators()
    {
        return new List<MathOperator>
            {
                new BinaryMathOperator
                (
                    "+",
                    MathOperatorPrecedence.AdditionSubtraction,
                    (right, left) =>
                    {
                        return (right.ToDouble() + left.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "-",
                    MathOperatorPrecedence.AdditionSubtraction,
                    (right, left) =>
                    {
                        return (right.ToDouble() - left.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "*",
                    MathOperatorPrecedence.MultiplicationDivision,
                    (right, left) =>
                    {
                        return (right.ToDouble() * left.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "/",
                    MathOperatorPrecedence.MultiplicationDivision,
                    (right, left) =>
                    {
                        return (right.ToDouble() / left.ToDouble()).ToString();
                    }
                ),
                new PrefixUnaryMathOperator
                (
                    "-",
                    MathOperatorPrecedence.Unary,
                    input =>
                    {
                        return (-input.ToDouble()).ToString();
                    }
                ),
                //new FunctionMathOperator
                //(
                //    "-",
                //    MathOperatorPrecedence.NegativeSign,
                //    arguments =>
                //    {
                //        if (arguments.Count() != 1)
                //        {
                //            throw new IncorrectArgumentCountException("\"-\" takes one argument");
                //        }
                //        return (-arguments.First().ToDouble()).ToString();
                //    }
                //),
                new PostfixUnaryMathOperator
                (
                    "!",
                    MathOperatorPrecedence.Exponentiation,
                    input =>
                    {
                        return ((long)input.ToDouble()).Factorial().ToString();
                    }
                ),
                new PostfixUnaryMathOperator
                (
                    "torad",
                    MathOperatorPrecedence.Exponentiation,
                    input =>
                    {
                        return input.ToDouble().Deg2Rad().ToString();
                    }
                ),
                new PostfixUnaryMathOperator
                (
                    "todeg",
                    MathOperatorPrecedence.Exponentiation,
                    input =>
                    {
                        return input.ToDouble().Rad2Deg().ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "^",
                    MathOperatorPrecedence.Exponentiation,
                    (right, left) =>
                    {
                        return right.ToDouble().ToThePowerOf(left.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "%",
                    MathOperatorPrecedence.MultiplicationDivision,
                    (right, left) =>
                    {
                        return (right.ToDouble() % left.ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sqrt",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"sqrt\" takes 1 argument");
                        }
                        return Math.Sqrt(double.Parse(arguments.First())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sin",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"sin\" takes 1 argument");
                        }
                        return Math.Sin(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "asin",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"asin\" takes 1 argument");
                        }
                        return Math.Asin(arguments.First().ToDouble()).Rad2Deg().ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "cos",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"cos\" takes 1 argument");
                        }
                        return Math.Cos(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acos",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acos\" takes 1 argument");
                        }
                        return Math.Acos(arguments.First().ToDouble()).Rad2Deg().ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "tan",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"tan\" takes 1 argument");
                        }
                        return Math.Tan(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "atan",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"atan\" takes 1 argument");
                        }
                        return Math.Atan(arguments.First().ToDouble()).Rad2Deg().ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "csc",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"csc\" takes 1 argument");
                        }
                        return (1 / Math.Sin(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acsc",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acsc\" takes 1 argument");
                        }
                        return Math.Asin(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sec",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"sec\" takes 1 argument");
                        }
                        return (1 / Math.Cos(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "asec",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"asec\" takes 1 argument");
                        }
                        return Math.Acos(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "cot",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"cot\" takes 1 argument");
                        }
                        return (1 / Math.Tan(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acot",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acot\" takes 1 argument");
                        }
                        return Math.Atan(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sinh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"sinh\" takes 1 argument");
                        }
                        return Math.Sinh(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "asinh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"asinh\" takes 1 argument");
                        }
                        return Math.Asinh(arguments.First().ToDouble().Rad2Deg()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "cosh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"cosh\" takes 1 argument");
                        }
                        return Math.Cosh(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acosh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acosh\" takes 1 argument");
                        }
                        return Math.Acosh(arguments.First().ToDouble()).Rad2Deg().ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "tanh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"tanh\" takes 1 argument");
                        }
                        return Math.Tanh(arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "atanh",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"atanh\" takes 1 argument");
                        }
                        return Math.Atanh(arguments.First().ToDouble()).Rad2Deg().ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "csch",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"csch\" takes 1 argument");
                        }
                        return (1 / Math.Sinh(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acsch",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acsch\" takes 1 argument");
                        }
                        return Math.Asinh(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sech",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"sech\" takes 1 argument");
                        }
                        return (1 / Math.Cosh(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "asech",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"asech\" takes 1 argument");
                        }
                        return Math.Acosh(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "coth",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"coth\" takes 1 argument");
                        }
                        return (1 / Math.Tanh(arguments.First().ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acoth",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"acot\" takes 1 argument");
                        }
                        return Math.Atanh(1 / arguments.First().ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "P",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 2)
                        {
                            throw new IncorrectArgumentCountException("\"P\" takes 2 arguments");
                        }
                        string[] argumentsArray = arguments.ToArray();
                        long n = (long)argumentsArray[0].ToDouble();
                        long r = (long)argumentsArray[1].ToDouble();
                        return Utilities.Permutation(n, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "C",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 2)
                        {
                            throw new IncorrectArgumentCountException("\"C\" takes 2 arguments");
                        }
                        string[] argumentsArray = arguments.ToArray();
                        long n = (long)argumentsArray[0].ToDouble();
                        long r = (long)argumentsArray[1].ToDouble();
                        return Utilities.Combination(n, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "H",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 2)
                        {
                            throw new IncorrectArgumentCountException("\"H\" takes 2 arguments");
                        }
                        string[] argumentsArray = arguments.ToArray();
                        long n = (long)argumentsArray[0].ToDouble();
                        long r = (long)argumentsArray[1].ToDouble();
                        return Utilities.Combination(n + r - 1, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 2)
                        {
                            throw new IncorrectArgumentCountException("\"log\" takes 2 arguments");
                        }
                        string[] argumentsArray = arguments.ToArray();
                        return Utilities.Log(argumentsArray[0].ToDouble(), argumentsArray[1].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log10",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"log10\" takes 1 argument");
                        }
                        return Math.Log10(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log2",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"log\" takes 1 argument");
                        }
                        return Math.Log2(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "ln",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"ln\" takes 1 argument");
                        }
                        return Math.Log(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "ceil",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"ceil\" takes 1 argument");
                        }
                        return Math.Ceiling(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "floor",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"floor\" takes 1 argument");
                        }
                        return Math.Floor(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "round",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"round\" takes 1 argument");
                        }
                        return Math.Round(arguments.First().ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "abs",
                    MathOperatorPrecedence.Function,
                    arguments =>
                    {
                        if (arguments.Count() != 1)
                        {
                            throw new IncorrectArgumentCountException("\"abs\" takes 1 arguemnt");
                        }
                        return Math.Abs(arguments.First().ToDouble()).ToString();
                    }
                ),
                new ConstantMathOperator
                (
                    "pi",
                    MathOperatorPrecedence.Constant,
                    Math.PI.ToString()
                ),
                new ConstantMathOperator
                (
                    "e",
                    MathOperatorPrecedence.Constant,
                    Math.E.ToString()
                )
            };
    }

    /// <summary>
    /// Returns a <see cref="IEnumerable{MathOperator}"/> of invalid <see cref="MathOperator"/>s.
    /// </summary>
    /// <remarks>
    /// A <see cref="MathOperator"/> is valid if its <see cref="Name"/> only:
    /// <list type="bullet">
    /// <item>Contains letters, numbers, underscores and symbols(excluding '(', ')', ',' and '.')</item>
    /// <item>Doesn't start with a number</item>
    /// <item>Has a <see cref="string.Length"/> of 1 if it contains a symbol</item>
    /// <item>Doesn't include any whitespaces</item>
    /// </list>
    /// </remarks>
    /// <param name="operators"></param>
    /// <returns><see cref="IEnumerable{MathOperator}"/> of invalid <see cref="MathOperator"/>s.</returns>
    public static IEnumerable<MathOperator> GetInvalidOperators(IEnumerable<MathOperator> operators)
    {
        if (operators is null)
        {
            throw new ArgumentNullException($"{nameof(operators)}");
        }
        List<MathOperator> result = new();
        foreach (MathOperator mathOperator in operators)
        {
            if (mathOperator is null)
            {
                throw new ArgumentException($"Null element in {nameof(operators)}");
            }
            if (!IsValidOperatorName(mathOperator.Name))
            {
                result.Add(mathOperator);
            }
        }
        return result;
    }

    public static bool IsReservedSymbol(char c)
    {
        if (c is '(' or ')' or ',' or '.')
        {
            return true;
        }
        return false;
    }

    public static bool IsValidOperatorName(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException($"{nameof(name)}");
        }
        if (name[0].IsDigit())
        {
            return false;
        }
        for (int i = 0; i < name.Length; i++)
        {
            if ((!name[i].IsDigit() && !name[i].IsLetter() && name[i] != '_' && name.Length != 1) || IsReservedSymbol(name[i]) || name[i].IsWhitespace())
            {
                return false;
            }
        }
        return true;
    }
}
