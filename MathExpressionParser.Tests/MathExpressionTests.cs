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
                "log(log(3.2e+2, -1e1), sqrt(1E-4))",
                new()
                {
                    new FunctionalOperatorToken("log", 0, -1),
                    new OpeningParenthesisToken(3),
                    new FunctionalOperatorToken("log", 4, -1),
                    new OpeningParenthesisToken(7),
                    new NumberToken("3.2e+2", 8, 3.2e+2),
                    new CommaToken(14),
                    new PrefixUnaryOperatorToken("-", 16),
                    new NumberToken("1e1", 17, 1e1),
                    new ClosingParenthesisToken(20),
                    new CommaToken(21),
                    new FunctionalOperatorToken("sqrt", 23, -1),
                    new OpeningParenthesisToken(27),
                    new NumberToken("1E-4", 28, 1e-4),
                    new ClosingParenthesisToken(32),
                    new ClosingParenthesisToken(33)
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
            },
            {
                "\t ",
                new()
            },
            {
                "---1",
                new()
                {
                    new PrefixUnaryOperatorToken("-", 0),
                    new PrefixUnaryOperatorToken("-", 1),
                    new PrefixUnaryOperatorToken("-", 2),
                    new NumberToken("1", 3, 1)
                }
            },
            {
                "1---1",
                new()
                {
                    new NumberToken("1", 0, 1),
                    new BinaryOperatorToken("-", 1),
                    new PrefixUnaryOperatorToken("-", 2),
                    new PrefixUnaryOperatorToken("-", 3),
                    new NumberToken("1", 4, 1)
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

    [TestMethod]
    public void Validate_GettingResult_CorrectResult()
    {
        List<(string, ParserExceptionContext)> tuples0 = new()
        {
            new
            (
                "-pi * -sin(-e torad)",
                null
            ),
            new
            (
                "log(log(3.2e+2, -1e1), sqrt(1E-4))",
                null
            ),
            new
            (
                "\t\t  \n",
                null
            ),
            new
            (
                "1e + 1",
                new(0, ParserExceptionType.InvalidNumberFormat)
            ),
            new
            (
                "1 1",
                new(2, ParserExceptionType.UnexpectedNumber)
            ),
            new
            (
                "-pi * -sin(-e torad",
                new(19, ParserExceptionType.TooManyOpeningParentheses)
            ),
            new
            (
                "((1 + 1 +))",
                new(9, ParserExceptionType.UnexpectedClosingParenthesis)
            ),
            new
            (
                "1++",
                new(2, ParserExceptionType.UnexpectedBinaryOperator)
            ),
            new
            (
                "1 + (2! ^ 3)) + sin(5)",
                new(12, ParserExceptionType.UnexpectedClosingParenthesis)
            ),
            new
            (
                "1 + (2! ^ 3) + sin(5))",
                new(21, ParserExceptionType.UnexpectedClosingParenthesis)
            ),
            new
            (
                "1(1+1",
                new(1, ParserExceptionType.UnexpectedOpeningParenthesis)
            ),
            new
            (
                "1 + 1, 1 + 1",
                new(5, ParserExceptionType.UnexpectedComma)
            ),
            new
            (
                "log((1, 1))",
                new(6, ParserExceptionType.UnexpectedComma)
            ),
            new
            (
                "log((, 1))",
                new(5, ParserExceptionType.UnexpectedComma)
            ),
            new
            (
                "sin(,1)",
                new(4, ParserExceptionType.UnexpectedComma)
            ),
            new
            (
                "sin(1,)",
                new(6, ParserExceptionType.UnexpectedClosingParenthesis)
            ),
            new
            (
                "log(1, log(1, log(1, 1)))",
                null
            ),
            new
            (
                "log(1, log(log(1, 1), log(1, 1)))",
                null
            ),
            new
            (
                "sin(pi pi)",
                new(7, ParserExceptionType.UnexpectedConstantOperator)
            ),
            new
            (
                "sin(pipi)",
                new(4, ParserExceptionType.UnknownOperator)
            ),
            new
            (
                "sinn(pi)",
                new(0, ParserExceptionType.UnknownOperator)
            ),
            new
            (
                "2sin(1)",
                new(1, ParserExceptionType.UnexpectedFunctionalOperator)
            ),
            new
            (
                "1 1",
                new(2, ParserExceptionType.UnexpectedNumber)
            ),
            new
            (
                "sin((1)",
                new(7, ParserExceptionType.TooManyOpeningParentheses)
            ),
            new
            (
                "sin((((((1))))))",
                null
            ),
            new
            (
                "10!!",
                null
            ),
            new
            (
                "1*!",
                new(2, ParserExceptionType.UnexpectedPostfixUnaryOperator)
            ),
            new
            (
                "--1",
                new(1, ParserExceptionType.UnexpectedPrefixUnaryOperator)
            ),
            new
            (
                "sin(1",
                new(5, ParserExceptionType.TooManyOpeningParentheses)
            ),
            new
            (
                "1 +",
                new(3, ParserExceptionType.UnexpectedNewline)
            ),
            new
            (
                "sin(1, 1)",
                new(0, ParserExceptionType.IncorrectArgumentCount)
            ),
            new
            (
                "sin()",
                new(0, ParserExceptionType.IncorrectArgumentCount)
            )
        };
        foreach ((string, ParserExceptionContext) tuple in tuples0)
        {
            MathExpression mathExpression = new(tuple.Item1);
            ParserExceptionContext result = mathExpression.Validate()?.Context;
            if (tuple.Item2 != result)
            {
                Assert.Fail($"{tuple.Item1} Expected: {tuple.Item2 ?? (object)"null"} Actual: {result ?? (object)"null"}");
            }
        }
        FunctionalOperator functionWithInvalidName = new("1a", nums => nums[0]);
        FunctionalOperator function = new("f", nums => nums[0]);
        FunctionalOperator sin = new("sin", nums => nums[0]);
        ConstantOperator constantWithInvalidName = new("$", 0);
        ConstantOperator constant = new("x", 0);
        ConstantOperator pi = new("pi", 0);
        List<(MathExpression, ParserExceptionContext)> tuples1 = new()
        {
            new
            (
                new(functionWithInvalidName.ToSingletonList(), null),
                new(-1, ParserExceptionType.InvalidCustomFunctionName)
            ),
            new
            (
                new(((FunctionalOperator)null).ToSingletonList(), null),
                new(-1, ParserExceptionType.NullCustomFunction)
            ),
            new
            (
                new(null, constantWithInvalidName.ToSingletonList()),
                new(-1, ParserExceptionType.InvalidCustomConstantName)
            ),
            new
            (
                new(null, ((ConstantOperator)null).ToSingletonList()),
                new(-1, ParserExceptionType.NullCustomConstant)
            ),
            new
            (
                new(new List<FunctionalOperator> { function, function }, null),
                new(-1, ParserExceptionType.DuplicateCustomFunctions)
            ),
            new
            (
                new(sin.ToSingletonList(), null),
                new(-1, ParserExceptionType.DuplicateCustomFunctions)
            ),
            new
            (
                new(null, new List<ConstantOperator> { constant, constant }),
                new(-1, ParserExceptionType.DuplicateCustomConstants)
            ),
            new
            (
                new(null, pi.ToSingletonList()),
                new(-1, ParserExceptionType.DuplicateCustomConstants)
            )
        };
        foreach ((MathExpression, ParserExceptionContext) tuple in tuples1)
        {
            Assert.AreEqual(tuple.Item2, tuple.Item1.Validate().Context);
        }
    }

    [TestMethod]
    public void CalculateArgumentCount_GettingResult_CorrectResult()
    {
        List<(List<Token>, List<Token>)> tuples = new()
        {
            new
            (
                new()
                {
                    new FunctionalOperatorToken("log", 0, -1),
                    new OpeningParenthesisToken(3),
                    new FunctionalOperatorToken("log", 4, -1),
                    new OpeningParenthesisToken(7),
                    new NumberToken("3.2e+2", 8, 3.2e+2),
                    new CommaToken(14),
                    new PrefixUnaryOperatorToken("-", 16),
                    new NumberToken("1e1", 17, 1e1),
                    new ClosingParenthesisToken(20),
                    new CommaToken(21),
                    new FunctionalOperatorToken("sqrt", 23, -1),
                    new OpeningParenthesisToken(27),
                    new NumberToken("1E-4", 28, 1e-4),
                    new ClosingParenthesisToken(32),
                    new ClosingParenthesisToken(33)
                },
                new()
                {
                    new FunctionalOperatorToken("log", 0, 2),
                    new OpeningParenthesisToken(3),
                    new FunctionalOperatorToken("log", 4, 2),
                    new OpeningParenthesisToken(7),
                    new NumberToken("3.2e+2", 8, 3.2e+2),
                    new CommaToken(14),
                    new PrefixUnaryOperatorToken("-", 16),
                    new NumberToken("1e1", 17, 1e1),
                    new ClosingParenthesisToken(20),
                    new CommaToken(21),
                    new FunctionalOperatorToken("sqrt", 23, 1),
                    new OpeningParenthesisToken(27),
                    new NumberToken("1E-4", 28, 1e-4),
                    new ClosingParenthesisToken(32),
                    new ClosingParenthesisToken(33)
                }
            )
        };
        PrivateType mathExpression = new(typeof(MathExpression));
        foreach ((List<Token>, List<Token>) tuple in tuples)
        {
            List<Token> result = (List<Token>)mathExpression.InvokeStatic("CalculateArgumentCount", tuple.Item1);
            if (!tuple.Item2.SequenceEqual(result))
            {
                Assert.Fail($"Actual: {string.Join(", ", result.Where(token => token is FunctionalOperatorToken).Select(token => ((FunctionalOperatorToken)token).ArgumentCount))}");
            }
        }
    }
}