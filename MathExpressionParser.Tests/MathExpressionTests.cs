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
        Dictionary<string, List<Token>> expressionToTokens = new()
        {
            {
                "123 + 456",
                new()
                {
                    new("123", 0),
                    new("+", 4),
                    new("456", 6)
                }
            },
            {
                "123+456",
                new()
                {
                    new("123", 0),
                    new("+", 3),
                    new("456", 4)
                }
            },
            {
                "123-456",
                new()
                {
                    new("123", 0),
                    new("-", 3),
                    new("456", 4)
                }
            },
            {
                "123 + (-456)",
                new()
                {
                    new("123", 0),
                    new("+", 4),
                    new("(", 6),
                    new("-", 7),
                    new("456", 8),
                    new(")", 11)
                }
            },
            {
                "log10(sqrt(1E4))",
                new()
                {
                    new("log10", 0),
                    new("(", 5),
                    new("sqrt", 6),
                    new("(", 10),
                    new("1E4", 11),
                    new(")", 14),
                    new(")", 15)
                }
            },
            {
                "log(3.2e+2, sqrt(1E-4))",
                new()
                {
                    new("log", 0),
                    new("(", 3),
                    new("3.2e+2", 4),
                    new(",", 10),
                    new("sqrt", 12),
                    new("(", 16),
                    new("1E-4", 17),
                    new(")", 21),
                    new(")", 22)
                }
            },
            {
                "1 + (2 * (3 + 4))",
                new()
                {
                    new("1", 0),
                    new("+", 2),
                    new("(", 4),
                    new("2", 5),
                    new("*", 7),
                    new("(", 9),
                    new("3", 10),
                    new("+", 12),
                    new("4", 14),
                    new(")", 15),
                    new(")", 16)
                }
            },
        };
        foreach (KeyValuePair<string, List<Token>> pair in expressionToTokens)
        {
            List<Token> tokens = null;
            try
            {
                PrivateType mathExpression = new(typeof(MathExpression));
                tokens = (List<Token>)mathExpression.InvokeStatic("Tokenize", pair.Key);
            }
            catch (Exception e)
            {
                Assert.Fail($"{e.Message} {pair.Key}");
            }
            StringBuilder tokensExpanded = new();
            foreach (Token token in tokens)
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
        PrivateType mathExpression = new(typeof(MathExpression));
        mathExpression.InvokeStatic("Tokenize", (string)null);
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
            PrivateType mathExpression = new(typeof(MathExpression));
            TestUtilities.AssertException<ParserException>
            (
                () => mathExpression.InvokeStatic("Tokenize", tuple.Item1),
                e => e.Context.Position == tuple.Item2 && e.Context.Type == ParserExceptionType.InvalidNumberFormat,
                $"It should be thrown when the format of a number is invalid.{tuple}"
            );
        }
    }
}