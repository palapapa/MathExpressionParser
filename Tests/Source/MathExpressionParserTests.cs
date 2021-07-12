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
        public void Parse_ParsingMathExpression_CorrectResult()
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
        [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null", AllowDerivedTypes = false)]
        public void Parse_NullInput_ArgumentNullException()
        {
            Parse(null, MathOperator.GetDefaultOperators());
        }

        [TestMethod]
        public void Parse_InvalidExpression_ArgumentException()
        {
            List<string> invalidExpressions = new()
            {
                "+",
                "1 @ 1",
                "1 -",
                "bruh(123)",
                "1 2 3",
                "1 * (1 + 2))",
                "(1 * (1 + 2)",
                "+-",
                "+ 1"
            };
            foreach (string expression in invalidExpressions)
            {
                try
                {
                    Parse(expression, MathOperator.GetDefaultOperators());
                    Assert.Fail($"{nameof(ArgumentException)} is not thrown on invalid input", expression);
                }
                catch (ArgumentException)
                {
                    // ok: ArgumentException is thrown
                }
            }
        }

        [TestMethod]
        public void Tokenize_Tokenizing_CorrectResult()
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
                    Assert.Fail($"Tokens not correct: {pair.Key} => {tokensExpanded}");
                }
            }
        }

        [TestMethod]
        public void GetClosingParenthesisIndex_GettingIndex_CorrectResult()
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
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when there is too many opening parentheses", AllowDerivedTypes = false)]
        public void GetClosingParenthesisIndex_TooManyOpeningParentheses_ArgumentException()
        {
            List<string> tokens = new()
            {
                "(", "(", "a", "(", "b", "(", "c", ")", ")", "(", "(", "e", ")", "d", ")", ")", "(", "f", ")"
            }; // ((a(b(c))((e)d))(f)
            GetClosingParenthesisIndex(tokens, 0);
        }

        [TestMethod]
        public void GetOpeningParenthesisIndex_GettingIndex_CorrectResult()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when there is too many closing parentheses", AllowDerivedTypes = false)]
        public void GetOpeningParenthesisIndex_TooManyClosingParentheses_ArgumentException()
        {

        }
    }
}
