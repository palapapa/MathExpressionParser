using System;
using System.Collections.Generic;

namespace MEP
{
    public class MathOperator
    {
        /// <summary>
        /// The <see cref="string"/> representation of the <see cref="MathOperator"/> in a math expression. <br/>
        /// It can only contain letters, numbers, underscores and symbols(excluding '(', ')', ',' and '.'). It cannot start with a number. If it contains a symbol, then it must have a <see cref="string.Length"/> of 1.
        /// </summary>
        public string Name { get; set; }
        public MathOperatorPrecedence Priority { get; set; }

        protected MathOperator() { }

        public static IList<MathOperator> GetDefaultOperators()
        {
            return new List<MathOperator>
            {
                new BinaryMathOperator
                (
                    "+",
                    MathOperatorPrecedence.AdditionSubtraction,
                    (string input1, string input2) =>
                    {
                        return (input1.ToDouble() + input2.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "-",
                    MathOperatorPrecedence.AdditionSubtraction,
                    (string input1, string input2) =>
                    {
                        return (input1.ToDouble() - input2.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "*",
                    MathOperatorPrecedence.MultiplicationDivision,
                    (string input1, string input2) =>
                    {
                        return (input1.ToDouble() * input2.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "/",
                    MathOperatorPrecedence.MultiplicationDivision,
                    (string input1, string input2) =>
                    {
                        return (input1.ToDouble() / input2.ToDouble()).ToString();
                    }
                ),
                new UnaryMathOperator
                (
                    "-",
                    MathOperatorPrecedence.NegativeSign,
                    (string input) =>
                    {
                        return (-input.ToDouble()).ToString();
                    }
                ),
                new BinaryMathOperator
                (
                    "^",
                    MathOperatorPrecedence.Exponent,
                    (string input1, string input2) =>
                    {
                        return input1.ToDouble().ToThePowerOf(input2.ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sqrt",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"sqrt\" takes 1 argument");
                        }
                        return Math.Sqrt(double.Parse(arguments[0])).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sin",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"sin\" takes 1 argument");
                        }
                        return Math.Sin(arguments[0].ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "asin",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"asin\" takes 1 argument");
                        }
                        return Math.Sin(Math.Asin(arguments[0].ToDouble()).Rad2Deg()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "cos",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"cos\" takes 1 argument");
                        }
                        return Math.Cos(arguments[0].ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "acos",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"acos\" takes 1 argument");
                        }
                        return Math.Sin(Math.Acos(arguments[0].ToDouble()).Rad2Deg()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "tan",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"tan\" takes 1 argument");
                        }
                        return Math.Tan(arguments[0].ToDouble().Deg2Rad()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "atan",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"atan\" takes 1 argument");
                        }
                        return Math.Sin(Math.Atan(arguments[0].ToDouble()).Rad2Deg()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "csc",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"csc\" takes 1 argument");
                        }
                        return (1 / Math.Sin(arguments[0].ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "sec",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"sec\" takes 1 argument");
                        }
                        return (1 / Math.Cos(arguments[0].ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "cot",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"cot\" takes 1 argument");
                        }
                        return (1 / Math.Tan(arguments[0].ToDouble().Deg2Rad())).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "P",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 2)
                        {
                            throw new ArgumentException("\"P\" takes 2 arguments");
                        }
                        long n = (long)arguments[0].ToDouble();
                        long r = (long)arguments[1].ToDouble();
                        return Utilities.Permutation(n, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "C",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 2)
                        {
                            throw new ArgumentException("\"C\" takes 2 arguments");
                        }
                        long n = (long)arguments[0].ToDouble();
                        long r = (long)arguments[1].ToDouble();
                        return Utilities.Combination(n, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "H",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 2)
                        {
                            throw new ArgumentException("\"H\" takes 2 arguments");
                        }
                        long n = (long)(arguments[0].ToDouble() + arguments[1].ToDouble() - 1);
                        long r = (long)arguments[1].ToDouble();
                        return Utilities.Combination(n + r - 1, r).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 2)
                        {
                            throw new ArgumentException("\"log\" takes 2 arguments");
                        }
                        return Utilities.Log(arguments[0].ToDouble(), arguments[1].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log10",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"log10\" takes 1 argument");
                        }
                        return Math.Log10(arguments[0].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "log2",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"log\" takes 1 argument");
                        }
                        return Math.Log2(arguments[0].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "ln",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"ln\" takes 1 argument");
                        }
                        return Math.Log(arguments[0].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "ceil",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"ceil\" takes 1 argument");
                        }
                        return Math.Ceiling(arguments[0].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "floor",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"floor\" takes 1 argument");
                        }
                        return Math.Floor(arguments[0].ToDouble()).ToString();
                    }
                ),
                new FunctionMathOperator
                (
                    "round",
                    MathOperatorPrecedence.Function,
                    (IList<string> arguments) =>
                    {
                        if (arguments.Count != 1)
                        {
                            throw new ArgumentException("\"round\" takes 1 argument");
                        }
                        return Math.Round(arguments[0].ToDouble()).ToString();
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
        /// Returns a <see cref="IList{MathOperator}"/> of invalid <see cref="MathOperator"/>s. A <see cref="MathOperator"/> is valid if its <see cref="Name"/> only:
        /// <list type="bullet">
        /// <item>Contains letters, numbers, underscores and symbols(excluding '(', ')', ',' and '.')</item>
        /// <item>Doesn't start with a number</item>
        /// <item>Has a <see cref="string.Length"/> of 1 if it contains a symbol</item>
        /// </list>
        /// </summary>
        /// <param name="operators"></param>
        /// <returns><see cref="IList{MathOperator}"/> of invalid <see cref="MathOperator"/>s.</returns>
        public static IList<MathOperator> GetInvalidOperators(IList<MathOperator> operators)
        {
            IList<MathOperator> result = new List<MathOperator>();
            foreach (MathOperator mathOperator in operators)
            {
                if (!IsValidOperatorName(mathOperator.Name))
                {
                    result.Add(mathOperator);
                }
            }
            return result;
        }

        public static bool IsReservedSymbol(char c)
        {
            if
            (
                c == '(' ||
                c == ')' ||
                c == ',' ||
                c == '.'
            )
            {
                return true;
            }
            return false;
        }

        public static bool IsValidOperatorName(string name)
        {
            if (name[0].IsDigit())
            {
                return false;
            }
            for (int i = 0; i < name.Length; i++)
            {
                if ((!name[i].IsDigit() && !name[i].IsLetter() && name[i] != '_' && name.Length != 1) || IsReservedSymbol(name[i]) || name[i].IsWhiteSpace())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
