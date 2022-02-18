using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace MathExpressionParser.Tests;

[TestClass]
public class MathExpressionTests
{
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
                "123+456",
                new List<string>
                {
                    "123",
                    "+",
                    "456"
                }
            },
            {
                "123-456",
                new List<string>
                {
                    "123",
                    "-",
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
                "log(3.2e+2, sqrt(1E-4))",
                new List<string>
                {
                    "log",
                    "(",
                    "3.2e+2",
                    ",",
                    " ",
                    "sqrt",
                    "(",
                    "1E-4",
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
            List<string> tokens = null;
            try
            {
                PrivateObject mathExpression = new(new MathExpression(pair.Key));
                tokens = (List<string>)mathExpression.Invoke("Tokenize");
            }
            catch (Exception e)
            {
                Assert.Fail($"{e.Message} {pair.Key}");
            }
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
        PrivateObject mathExpression = new(new MathExpression((string)null));
        mathExpression.Invoke("Tokenize");
    }

    [TestMethod]
    public void Tokenize_InvalidNumberFormat_ParserException()
    {
        List<(string, int)> tuples = new()
        {
            ("1 + 1ee", 4),
            ("3 + sin(1..e3)", 8),
            ("3 / (1e3..)", 5),
            ("2er3", 0),
            ("1 - 2.3E--2", 4)
        };
        foreach ((string, int) tuple in tuples)
        {
            PrivateObject mathExpression = new(new MathExpression(tuple.Item1));
            TestUtilities.AssertException<ParserException>
            (
                () => mathExpression.Invoke("Tokenize"),
                e => e.Context.ErrorPosition == tuple.Item2 && e.Context.Type == ParserExceptionType.InvalidNumberFormat,
                $"It should be thrown when the format of a number is invalid.{tuple}"
            );
        }
    }
}