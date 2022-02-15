using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace MEP.Tests;

[TestClass]
public class MathExpressionParserTests : MathExpressionParser
{
    [TestMethod]
    public void Parse_GettingResult_CorrectResult()
    {
        Dictionary<string, string> expressionToValue = new()
        {
            { "123 + 456", "579" },
            { "123 + (-456)", "-333" },
            { "123 + (-(456))", "-333" },
            { "(100 - 200) + (100 + 200)", "200" },
            { "1000 - 500", "500" },
            { "1 + -2", "-1" },
            { "10 * 100", "1000" },
            { "-10 * 100", "-1000" },
            { "10 / 4", "2.5" },
            { "2^3",  "8" },
            { "-2^3", "-8" },
            { "(-2)^3", "-8" },
            { "sqrt(9)", "3" },
            { "sqrt(2)", Math.Sqrt(2).ToString() },
            { "", "" },
            { "  ", "" },
            { "\t ", "" },
            { "\n", "" },
            { "()", "" },
            { "()()", "" },
            { "(())", "" },
            { "sin(90)", "1" },
            { "cos(90)", "0" },
            { "tan(45)", "1" },
            { "asin(sqrt(2) / 2)", "45" },
            { "acos(1)", "0" },
            { "atan(1)", "45" },
            { "csc(30)", "2" },
            { "sec(60)", "2" },
            { "cot(45)", "1" },
            { "P(5, 2)", "20" },
            { "C(5, 2)", "10" },
            { "H(5, 2)", "15" },
            { "log(64, 2)", "6" },
            { "log10(100)", "2" },
            { "log2(8)", "3" },
            { "ln(10)", Math.Log(10).ToString() },
            { "ceil(0.5)", "1" },
            { "floor(0.5)", "0" },
            { "round(1.5)", Math.Round(1.5).ToString() },
            { "pi", Math.PI.ToString() },
            { "e", Math.E.ToString() },
            { "1 + 2 * 3", "7" },
            { "sqrt(1 + sin(90) * 3)", "2" },
            { "log10(sqrt(1E4))", "2" },
            { "abs(-2)", "2" },
            { "9 % 2", "1" },
            { "5!", "120" },
            { "1 * (2 + 3) - (4 * 5 * (6 + 7))", "-255" },
            { "1! * 2! * 3! * 4!", "288" },
            { "e * 1e2", (Math.E * 100).ToString() },
            { "sin((pi / 2)todeg)", "1" }
        };
        foreach (KeyValuePair<string, string> pair in expressionToValue)
        {
            try
            {
                try
                {
                    Assert.AreEqual(Parse(pair.Key).ToDouble(), pair.Value.ToDouble(), 1E-7);
                }
                catch (FormatException)
                {
                    Assert.AreEqual(Parse(pair.Key), pair.Value);
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"{e.Message}\nExpression: {pair.Key} Expected: {pair.Value}");
            }
        }
    }

    [TestMethod]
    public void Parse_ArgumentNull_ArgumentNullException()
    {
        List<(string, IEnumerable<MathOperator>)> arguments = new()
        {
            new(null, MathOperator.GetDefaultOperators()),
            new("1 + 1", null),
            new(null, null)
        };
        foreach ((string, List<MathOperator>) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentNullException>
            (
                () => Parse(tuple.Item1, tuple.Item2),
                $"It should be thrown when some of the arguments are null."
            );
        }
    }

    [TestMethod]
    public void Parse_NullInArgument_ArgumentException()
    {
        TestUtilities.AssertException<ArgumentException>
        (
            () => Parse("1 + 1", MathOperator.GetDefaultOperators().Concat(((MathOperator)null).ToSingletonList())),
            $"It should be thrown when the provided {nameof(MathOperator)}s contain null."
        );
    }

    [TestMethod]
    public void Parse_InvalidExpression_InvalidExpressionException()
    {
        List<string> invalidExpressions = new()
        {
            "1 2 3"
        };
        foreach (string expression in invalidExpressions)
        {
            TestUtilities.AssertException<InvalidExpressionException>
            (
                () => Parse(expression),
                "It should be thrown on invalid input."
            );
        }
    }

    [TestMethod]
    public void Parse_OperandNotFound_OperandNotFoundException()
    {
        List<string> invalidExpressions = new()
        {
            "log(69,)",
            "log(,69)",
            "log(,)",
            "log(69,,420)", // this should trigger OperandNotFoundException before TooManyArgumentsException
            "(+-)",
            "1 + -"
        };
        foreach (string expression in invalidExpressions)
        {
            TestUtilities.AssertException<OperandNotFoundException>
            (
                () => Parse(expression),
                $"It should be thrown when the operands of a {nameof(MathOperator)} are missing."
            );
        }
    }

    [TestMethod]
    public void Parse_NoMatchingMathOperator_NoMatchingMathOperatorException()
    {
        List<string> invalidExpressions = new()
        {
            "+",
            "1 -",
            "+-",
            "+ 1",
            "1 sin 1",
            "cos 1",
            "2pi",
            "e 2",
            "2 pi 2",
            "+(1)"
        };
        foreach (string expression in invalidExpressions)
        {
            TestUtilities.AssertException<NoMatchingMathOperatorException>
            (
                () => Parse(expression),
                $"It should be thrown when no suitable overload of a {nameof(MathOperator)} is found."
            );
        }
    }

    [TestMethod]
    public void Parse_UnmatchedParenthesis_UnmatchedParenthesisException()
    {
        List<string> invalidExpressions = new()
        {
            "1 * (1 + 2))",
            "(1 * (1 + 2)",
            "(()",
            "sin(10"
        };
        foreach (string expression in invalidExpressions)
        {
            TestUtilities.AssertException<UnmatchedParenthesisException>
            (
                () => Parse(expression),
                $"It should be thrown when the expression contains too many opening or closing parentheses."
            );
        }
    }

    [TestMethod]
    public void Parse_InvalidMathOperatorName_InvalidMathOperatorNameException()
    {
        List<MathOperator> invalidOperators = new()
        {
            new BinaryMathOperator
            (
                "++",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "(",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                ")",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                ",",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                ".",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "()",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "),.",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "1a",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                " ",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "1",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            ),
            new BinaryMathOperator
            (
                "\t",
                MathOperatorPrecedence.AdditionSubtraction,
                null
            )
        };
        TestUtilities.AssertException<InvalidMathOperatorNameException>
        (
            () => Parse("1 + 1", invalidOperators.Concat(MathOperator.GetDefaultOperators()).ToList()),
            $"It should be thrown when the provided {nameof(MathOperator)}s contain invalid {nameof(MathOperator)} names."
        );
    }

    [TestMethod]
    public void Parse_DuplicateMathOperators_DuplicateMathOperatorException()
    {
        TestUtilities.AssertException<DuplicateMathOperatorException>
        (
            () => Parse("", new List<MathOperator>() { new BinaryMathOperator("a", MathOperatorPrecedence.AdditionSubtraction, null), new BinaryMathOperator("a", MathOperatorPrecedence.AdditionSubtraction, null) }),
            "It should be thrown when there are duplicate operator names in the provided operators"
        );
        List<MathOperator> operators = MathOperator.GetDefaultOperators().ToList();
        operators.Add(new BinaryMathOperator("pi", MathOperatorPrecedence.AdditionSubtraction, null));
        TestUtilities.AssertException<DuplicateMathOperatorException>
        (
            () => Parse("", operators),
            $"It should be thrown when a {nameof(ConstantMathOperator)} has another overload."
        );
    }

    [TestMethod]
    public void Parse_MathOperatorNotFound_MathOperatorNotFoundException()
    {
        TestUtilities.AssertException<MathOperatorNotFoundException>
        (
            () => Parse("1 @ 1"),
            $"It should be thrown when the expression contains an unknown {nameof(MathOperator)}."
        );
    }

    [TestMethod]
    public void Parse_IncorrectArgumentCount_IncorrectArgumentCountException()
    {
        TestUtilities.AssertException<IncorrectArgumentCountException>
        (
            () => Parse("sin(1, 1)"),
            $"It should be thrown when there are too many arguments passed to a {nameof(FunctionMathOperator)}."
        );
    }

    [TestMethod]
    public void Tokenize_GettingResult_CorrectResult()
    {
        Dictionary<string, List<string>> expressionToTokens = new()
        {
            {
                "123 + 456",
                new List<string>
                {
                    "123",
                    " ",
                    "+",
                    " ",
                    "456"
                }
            },
            {
                "123 + (-456)",
                new List<string>
                {
                    "123",
                    " ",
                    "+",
                    " ",
                    "(",
                    "-",
                    "456",
                    ")"
                }
            },
            {
                "log10(sqrt(1E4))",
                new List<string>
                {
                    "log10",
                    "(",
                    "sqrt",
                    "(",
                    "1E4",
                    ")",
                    ")"
                }
            },
            {
                "log(10, sqrt(1E4))",
                new List<string>
                {
                    "log",
                    "(",
                    "10",
                    ",",
                    " ",
                    "sqrt",
                    "(",
                    "1E4",
                    ")",
                    ")"
                }
            },
            {
                "1 + (2 * (3 + 4))",
                new List<string>
                {
                    "1",
                    " ",
                    "+",
                    " ",
                    "(",
                    "2",
                    " ",
                    "*",
                    " ",
                    "(",
                    "3",
                    " ",
                    "+",
                    " ",
                    "4",
                    ")",
                    ")"
                }
            },
        };
        foreach (KeyValuePair<string, List<string>> pair in expressionToTokens)
        {
            List<string> tokens = Tokenize(pair.Key);
            StringBuilder tokensExpanded = new();
            foreach (string token in tokens)
            {
                tokensExpanded.Append($"\"{token}\" ");
            }
            if (!tokens.SequenceEqual(pair.Value))
            {
                Assert.Fail($"Tokens not correct: {pair.Key} => {tokensExpanded}.");
            }
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
    public void Tokenize_ArgumentNull_ArgumentNullException()
    {
        Tokenize(null);
    }

    [TestMethod]
    public void Tokenize_InvalidNumberFormat_FormatException()
    {
        List<string> arguments = new()
        {
            "1ee",
            "1..e3",
            "2todeg",
            "1e3.."
        };
        foreach (string argument in arguments)
        {
            TestUtilities.AssertException<FormatException>
            (
                () => Tokenize(argument),
                $"It should be thrown when the format of a number is invalid.({argument})"
            );
        }
    }

    [TestMethod]
    public void GetClosingParenthesisIndex_GettingResult_CorrectResult()
    {
        List<string> tokens = new()
        {
            "(",
            "a",
            "(",
            "b",
            "(",
            "c",
            ")",
            ")",
            "(",
            "(",
            "e",
            ")",
            "d",
            ")",
            ")",
            "(",
            "f",
            ")"
        }; // (a(b(c))((e)d))(f)
        Dictionary<int, int> openToClose = new()
        {
            { 0, 14 },
            { 2, 7 },
            { 4, 6 },
            { 8, 13 },
            { 9, 11 },
            { 15, 17 }
        };
        foreach (KeyValuePair<int, int> pair in openToClose)
        {
            Assert.AreEqual(pair.Value, GetClosingParenthesisIndex(tokens, pair.Key));
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
    public void GetClosingParenthesisIndex_ArgumentNull_ArgumentNullException()
    {
        GetClosingParenthesisIndex(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(UnmatchedParenthesisException), "UnmatchedParenthesisException should be thrown when there are too many opening parentheses.", AllowDerivedTypes = false)]
    public void GetClosingParenthesisIndex_TooManyOpeningParentheses_UnmatchedParenthesisException()
    {
        List<string> tokens = new()
        {
            "(",
            "(",
            "a",
            "(",
            "b",
            "(",
            "c",
            ")",
            ")",
            "(",
            "(",
            "e",
            ")",
            "d",
            ")",
            ")",
            "(",
            "f",
            ")"
        }; // ((a(b(c))((e)d))(f)
        GetClosingParenthesisIndex(tokens, 0);
    }

    [TestMethod]
    public void GetOpeningParenthesisIndex_GettingResult_CorrectResult()
    {
        List<string> tokens = new()
        {
            "(",
            "a",
            "(",
            "b",
            "(",
            "c",
            ")",
            ")",
            "(",
            "(",
            "e",
            ")",
            "d",
            ")",
            ")",
            "(",
            "f",
            ")"
        }; // (a(b(c))((e)d))(f)
        Dictionary<int, int> closeToOpen = new()
            {
                { 14, 0 },
                { 7, 2 },
                { 6, 4 },
                { 13, 8 },
                { 11, 9 },
                { 17, 15 }
            };
        foreach (KeyValuePair<int, int> pair in closeToOpen)
        {
            Assert.AreEqual(pair.Value, GetOpeningParenthesisIndex(tokens, pair.Key));
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
    public void GetOpeningParenthesisIndex_ArgumentNull_ArgumentNullException()
    {
        GetOpeningParenthesisIndex(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(UnmatchedParenthesisException), "UnmatchedParenthesisException should be thrown when there are too many closing parentheses.", AllowDerivedTypes = false)]
    public void GetOpeningParenthesisIndex_TooManyClosingParentheses_UnmatchedParenthesisException()
    {
        List<string> tokens = new()
        {
            "(",
            "a",
            "(",
            "b",
            "(",
            "c",
            ")",
            ")",
            "(",
            "(",
            "e",
            ")",
            "d",
            ")",
            ")",
            "(",
            "f",
            ")",
            ")"
        }; // ((a(b(c))((e)d))(f)
        GetOpeningParenthesisIndex(tokens, tokens.Count - 1);
    }

    [TestMethod]
    public void GetIndexInStringFromTokens_GettingResult_CorrectResult()
    {
        Assert.AreEqual(GetIndexInStringFromTokens(new List<string> { "123", "456", "789" }, 2, 1, 1), 9);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the first argument is null.", AllowDerivedTypes = false)]
    public void GetIndexInStringFromTokens_ArgumentNull_ArgumentNullException()
    {
        GetIndexInStringFromTokens(null, 0, 0, 0);
    }

    [TestMethod]
    public void GetIndexInStringFromTokens_OutOfRange_ArgumentOutOfRangeException()
    {
        List<string> tokens = new() { "123", "456", "789" };
        List<(List<string>, int, int, int)> arguments = new()
        {
            (tokens, 3, 0, 0),
            (tokens, 0, 3, 0),
            (tokens, 3, 3, 0),
            (tokens, -1, 0, 0),
            (tokens, 0, -1, -1)
        };
        foreach ((List<string>, int, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => GetIndexInStringFromTokens(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the indices are out of range."
            );
        }
    }

    [TestMethod]
    public void UnpackTokens_GettingResult_CorrectResult()
    {
        Assert.AreEqual(UnpackTokens(new List<string> { "123", "456", "789" }, " "), "123 456 789");
    }

    [TestMethod]
    public void UnpackTokens_ArgumentNull_ArgumentNullException()
    {
        List<(List<string>, string)> arguments = new()
        {
            (null, " "),
            (new List<string> { "123", "456", "789" }, null),
            (null, null)
        };
        foreach ((List<string>, string) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentNullException>
            (
                () => UnpackTokens(tuple.Item1, tuple.Item2),
                "It should be thrown when some of the arguments are null."
            );
        }
    }

    [TestMethod]
    public void GetSuitableMathOperatorType_GettingResult_CorrectResult()
    {
        List<(List<string>, int, Type)> arguments = new()
        {
            (
                new()
                {
                    "1",
                    " ",
                    "+",
                    " ",
                    "1"
                },
                2,
                typeof(BinaryMathOperator)
            ),
            (
                new()
                {
                    "(",
                    "1",
                    ")",
                    "+",
                    "(",
                    "1",
                    ")"
                },
                3,
                typeof(BinaryMathOperator)
            ),
            (
                new()
                {
                    "1",
                    "+",
                    "(",
                    "1",
                    ")"
                },
                1,
                typeof(BinaryMathOperator)
            ),
            (
                new()
                {
                    "pi",
                    "*",
                    "(",
                    "1",
                    "+",
                    "1",
                    ")"
                },
                0,
                typeof(ConstantMathOperator)
            ),
            (
                new()
                {
                    "pi",
                    "*",
                    "(",
                    "1",
                    "+",
                    "1",
                    ")"
                },
                1,
                typeof(BinaryMathOperator)
            ),
            (
                new()
                {
                    "e",
                    "*",
                    "sqrt",
                    "(",
                    "2",
                    ")"
                },
                2,
                typeof(FunctionMathOperator)
            ),
            (
                new()
                {
                    "e",
                    "*",
                    "sqrt",
                    "(",
                    "2",
                    ")"
                },
                1,
                typeof(BinaryMathOperator)
            ),
            (
                new()
                {
                    "1",
                    "+",
                    "-",
                    "2"
                },
                2,
                typeof(PrefixUnaryMathOperator)
            ),
            (
                new()
                {
                    "2",
                    "*",
                    "3",
                    "torad"
                },
                3,
                typeof(PostfixUnaryMathOperator)
            ),
            (
                new()
                {
                    "1"
                },
                0,
                null
            ),
            (
                new()
                {
                    " "
                },
                0,
                null
            )
        };
        foreach ((List<string>, int, Type) argument in arguments)
        {
            Assert.AreEqual(GetSuitableMathOperatorType(argument.Item1, argument.Item2, MathOperator.GetDefaultOperators().ToList()), argument.Item3, string.Join("", argument.Item1));
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), $"{nameof(ArgumentNullException)} should be thrown when the arguments are null.", AllowDerivedTypes = false)]
    public void GetSuitableMathOperatorType_ArgumentNull_ArgumentNullException()
    {
        GetSuitableMathOperatorType(null, 0, MathOperator.GetDefaultOperators().ToList());
        GetSuitableMathOperatorType(null, 0, null);
        GetSuitableMathOperatorType(new List<string> { "1", "+", "1" }, 0, MathOperator.GetDefaultOperators().ToList());
    }

    [TestMethod]
    public void GetSuitableMathOperatorType_OutOfRange_ArgumentOutOfRangeException()
    {
        List<(List<string>, int)> arguments = new()
        {
            (
                new()
                {
                    "1"
                },
                -1
            ),
            (
                new()
                {
                    "1"
                },
                1
            )
        };
        foreach ((List<string>, int) argument in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => { GetSuitableMathOperatorType(argument.Item1, argument.Item2, MathOperator.GetDefaultOperators().ToList()); },
                "It should be thrown when the index is out of range."
            );
        }
    }
}
