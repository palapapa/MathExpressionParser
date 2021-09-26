using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEP;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class MathExpressionParserTests : MathExpressionParser
    {
        [TestMethod]
        public void Parse_GettingResult_CorrectResult()
        {
            IReadOnlyDictionary<string, string> expressionToValue = new Dictionary<string, string>
            {
                { "123 + 456", "579" },
                { "123 + (-456)", "-333" },
                { "1000 - 500", "500" },
                { "10 * 100", "1000" },
                { "-10 * 100", "-1000" },
                { "10 / 4", "2.5" },
                { "2^3",  "8" },
                { "-2^3", "-8" },
                { "(-2)^3", "-8" },
                { "sqrt(9)", "3" },
                { "", "" },
                { "  ", "" },
                { "sin(90)", "1" },
                { "cos(90)", "0" },
                { "tan(45)", "1" },
                { "asin(1)", "90" },
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
                { "log10(sqrt(1E4))", "2" }
            };
            foreach (KeyValuePair<string, string> pair in expressionToValue)
            {
                Assert.AreEqual(Parse(pair.Key, MathOperator.GetDefaultOperators()), pair.Value);
            }
        }

        [TestMethod]
        public void Parse_ArgumentNull_ArgumentNullException()
        {
            List<(string, IList<MathOperator>)> arguments = new()
            {
                new(null, MathOperator.GetDefaultOperators()),
                new("1 + 1", null),
                new(null, null)
            };
            foreach ((string, IList<MathOperator>) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        Parse(tuple.Item1, tuple.Item2);
                    },
                    "It should be thrown on null input."
                );
            }
        }

        [TestMethod]
        public void Parse_InvalidExpression_InvalidExpressionException()
        {
            List<string> invalidExpressions = new()
            {
                "1 2 3",
                " ",
                "",
                "\t",
                "  "
            };
            foreach (string expression in invalidExpressions)
            {
                TestUtilities.AssertException<InvalidExpressionException>
                (
                    () =>
                    {
                        Parse(expression);
                    },
                    "It should be thrown on invalid input."
                );
            }
        }

        [TestMethod]
        public void Parse_OperandNotFound_OperandNotFoundException()
        {
            List<string> invalidExpressions = new()
            {
                "+",
                "1 -",
                "+-",
                "+ 1"
            };
            foreach (string expression in invalidExpressions)
            {
                TestUtilities.AssertException<OperandNotFoundException>
                (
                    () =>
                    {
                        Parse(expression);
                    },
                    $"It should be thrown when the operand(s) of a {nameof(MathOperator)} is missing."
                );
            }
        }

        [TestMethod]
        public void Parse_UnmatchedParenthesis_UnmatchedParenthesisException()
        {
            List<string> invalidExpressions = new()
            {
                "1 * (1 + 2))",
                "(1 * (1 + 2)"
            };
            foreach (string expression in invalidExpressions)
            {
                TestUtilities.AssertException<UnmatchedParenthesisException>
                (
                    () =>
                    {
                        Parse(expression);
                    },
                    $"It should be thrown when the expression contains too many opening or closing parentheses."
                );
            }
        }

        [TestMethod]
        public void Parse_InvalidMathOperators_InvalidMathOperatorException()
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
            TestUtilities.AssertException<InvalidMathOperatorException>
            (
                () =>
                {
                    Parse("1 + 1", invalidOperators.Concat(MathOperator.GetDefaultOperators()).ToList());
                },
                $"It should be thrown when the provided {nameof(MathOperator)}s contain invalid {nameof(MathOperator)}s."
            );
        }

        [TestMethod]
        public void Parse_MathOperatorNotFound_MathOperatorNotFoundException()
        {
            TestUtilities.AssertException<MathOperatorNotFoundException>
            (
                () =>
                {
                    Parse("1 @ 1");
                },
                $"It should be thrown when the expression contains an unknown {nameof(MathOperator)}."
            );
        }

        [TestMethod]
        public void Tokenize_GettingResult_CorrectResult()
        {
            IReadOnlyDictionary<string, IList<string>> expressionToTokens = new Dictionary<string, IList<string>>
            {
                {
                    "123 + 456",
                    new List<string>
                    {
                        "123",
                        "+",
                        "456"
                    }
                },
                {
                    "123 + (-456)",
                    new List<string>
                    {
                        "123",
                        "+",
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
                }
            };
            foreach (KeyValuePair<string, IList<string>> pair in expressionToTokens)
            {
                IList<string> tokens = Tokenize(pair.Key);
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
        public void GetClosingParenthesisIndex_GettingResult_CorrectResult()
        {
            List<string> tokens = new()
            {
                "(", "a", "(", "b", "(", "c", ")", ")", "(", "(", "e", ")", "d", ")", ")", "(", "f", ")"
            }; // (a(b(c))((e)d))(f)
            IReadOnlyDictionary<int, int> openToClose = new Dictionary<int, int>()
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
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when there are too many opening parentheses.", AllowDerivedTypes = false)]
        public void GetClosingParenthesisIndex_TooManyOpeningParentheses_ArgumentException()
        {
            List<string> tokens = new()
            {
                "(", "(", "a", "(", "b", "(", "c", ")", ")", "(", "(", "e", ")", "d", ")", ")", "(", "f", ")"
            }; // ((a(b(c))((e)d))(f)
            GetClosingParenthesisIndex(tokens, 0);
        }

        [TestMethod]
        public void GetOpeningParenthesisIndex_GettingResult_CorrectResult()
        {
            List<string> tokens = new()
            {
                "(", "a", "(", "b", "(", "c", ")", ")", "(", "(", "e", ")", "d", ")", ")", "(", "f", ")"
            }; // (a(b(c))((e)d))(f)
            IReadOnlyDictionary<int, int> closeToOpen = new Dictionary<int, int>()
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
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when there are too many closing parentheses.", AllowDerivedTypes = false)]
        public void GetOpeningParenthesisIndex_TooManyClosingParentheses_ArgumentException()
        {
            List<string> tokens = new()
            {
                "(", "a", "(", "b", "(", "c", ")", ")", "(", "(", "e", ")", "d", ")", ")", "(", "f", ")", ")"
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
        public void GetIndexInStringFromTokens_NegativeArguments_ArgumentException()
        {
            List<string> tokens = new List<string> { "123", "456", "789" };
            List<(List<string>, int, int, int)> arguments = new()
            {
                (tokens, -1, 0, 0),
                (tokens, 0, -1, -1),
                (tokens, 0, 0, -1),
            };
            foreach ((List<string>, int, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        GetIndexInStringFromTokens(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when some of the arguments are negative."
                );
            }
        }

        [TestMethod]
        public void GetIndexInStringFromTokens_OutOfRange_ArgumentOutOfRangeException()
        {
            List<string> tokens = new List<string> { "123", "456", "789" };
            List<(List<string>, int, int, int)> arguments = new()
            {
                (tokens, 3, 0, 0),
                (tokens, 0, 3, 0),
                (tokens, 3, 3, 0),
            };
            foreach ((List<string>, int, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentOutOfRangeException>
                (
                    () =>
                    {
                        GetIndexInStringFromTokens(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the indexes are out of range."
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
            foreach((List<string>, string) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        UnpackTokens(tuple.Item1, tuple.Item2);
                    },
                    "It should be thrown when some of the arguments are null."
                );
            }
        }
    }
}
