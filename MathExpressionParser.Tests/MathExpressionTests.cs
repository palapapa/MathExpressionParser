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
                    new NumberToken("123", 0, 123),
                    new BinaryOperatorToken("+", 4),
                    new NumberToken("456", 6, 456)
                }
            },
            {
                "123+456",
                new()
                {
                    new NumberToken("123", 0, 123),
                    new BinaryOperatorToken("+", 3),
                    new NumberToken("456", 4, 456)
                }
            },
            {
                "123-456",
                new()
                {
                    new NumberToken("123", 0, 123),
                    new BinaryOperatorToken("-", 3),
                    new NumberToken("456", 4, 456)
                }
            },
            {
                "123 + (-456)",
                new()
                {
                    new NumberToken("123", 0, 123),
                    new BinaryOperatorToken("+", 4),
                    new OpeningParenthesisToken(6),
                    new PrefixUnaryOperatorToken("-", 7),
                    new NumberToken("456", 8, 456),
                    new ClosingParenthesisToken(11)
                }
            },
            {
                "log10(sqrt(1E4))",
                new()
                {
                    new FunctionalOperatorToken("log10", 0, -1),
                    new OpeningParenthesisToken(5),
                    new FunctionalOperatorToken("sqrt", 6, -1),
                    new OpeningParenthesisToken(10),
                    new NumberToken("1E4", 11, 1e4),
                    new ClosingParenthesisToken(14),
                    new ClosingParenthesisToken(15)
                }
            },
            {
                "log(log(3.2e+2, 1e1), sqrt(1E-4))",
                new()
                {
                    new FunctionalOperatorToken("log", 0, -1),
                    new OpeningParenthesisToken(3),
                    new FunctionalOperatorToken("log", 4, -1),
                    new OpeningParenthesisToken(7),
                    new NumberToken("3.2e+2", 8, 3.2e+2),
                    new CommaToken(14),
                    new NumberToken("1e1", 16, 1e1),
                    new ClosingParenthesisToken(19),
                    new CommaToken(20),
                    new FunctionalOperatorToken("sqrt", 22, -1),
                    new OpeningParenthesisToken(26),
                    new NumberToken("1E-4", 27, 1e-4),
                    new ClosingParenthesisToken(31),
                    new ClosingParenthesisToken(32)
                }
            },
            {
                "1 + (2 * (3 + 4))",
                new()
                {
                    new NumberToken("1", 0, 1),
                    new BinaryOperatorToken("+", 2),
                    new OpeningParenthesisToken(4),
                    new NumberToken("2", 5, 2),
                    new BinaryOperatorToken("*", 7),
                    new OpeningParenthesisToken(9),
                    new NumberToken("3", 10, 3),
                    new BinaryOperatorToken("+", 12),
                    new NumberToken("4", 14, 4),
                    new ClosingParenthesisToken(15),
                    new ClosingParenthesisToken(16)
                }
            },
            {
                "-pi * -sin(-e torad)",
                new()
                {
                    new PrefixUnaryOperatorToken("-", 0),
                    new ConstantOperatorToken("pi", 1),
                    new BinaryOperatorToken("*", 4),
                    new PrefixUnaryOperatorToken("-", 6),
                    new FunctionalOperatorToken("sin", 7, -1),
                    new OpeningParenthesisToken(10),
                    new PrefixUnaryOperatorToken("-", 11),
                    new ConstantOperatorToken("e", 12),
                    new PostfixUnaryOperatorToken("torad", 14),
                    new ClosingParenthesisToken(19)
                }
            }
        };
        foreach (KeyValuePair<string, List<Token>> pair in expressionToTokens)
        {
            List<Token> tokens = null;
            try
            {
                PrivateObject mathExpression = new(new MathExpression(pair.Key));
                tokens = (List<Token>)mathExpression.Invoke("Tokenize");
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
                e => e.Context.Position == tuple.Item2 && e.Context.Type == ParserExceptionType.InvalidNumberFormat,
                $"It should be thrown when the format of a number is invalid.{tuple}"
            );
        }
    }
}