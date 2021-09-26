using System;
using System.Collections.Generic;
using MEP;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MathOperatorTests
    {
        [TestMethod]
        public void GetInvalidOperators_GettingResult_CorrectResult()
        {
            List<MathOperator> operators = new()
            {
                new BinaryMathOperator
                (
                    "+",
                    MathOperatorPrecedence.AdditionSubtraction,
                    null
                ),
                new BinaryMathOperator
                (
                    "-",
                    MathOperatorPrecedence.AdditionSubtraction,
                    null
                ),
                new BinaryMathOperator
                (
                    "*",
                    MathOperatorPrecedence.MultiplicationDivision,
                    null
                ),
                new BinaryMathOperator
                (
                    "/",
                    MathOperatorPrecedence.MultiplicationDivision,
                    null
                )
            };
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
            if (!MathOperator.GetInvalidOperators(operators.Concat(invalidOperators).ToList()).SequenceEqual(invalidOperators))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetInvalidOperators_ArgumentNull_ArgumentNullException()
        {
            List<List<MathOperator>> arguments = new()
            {
                new List<MathOperator>
                {
                    null
                },
                null
            };
            foreach (List<MathOperator> mathOperators in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        MathOperator.GetInvalidOperators(mathOperators);
                    },
                    $"It should be thrown when the argument is null or when the argument contains null {nameof(MathOperator)}s."
                );
            }
        }

        [TestMethod]
        public void IsReservedSymbol_GettingResult_CorrectResult()
        {
            List<(char, bool)> tuples = new()
            {
                ('(', true),
                (')', true),
                (',', true),
                ('.', true),
                ('+', false),
                ('a', false),
            };
            foreach ((char, bool) tuple in tuples)
            {
                Assert.AreEqual(MathOperator.IsReservedSymbol(tuple.Item1), tuple.Item2);
            }
        }

        [TestMethod]
        public void IsValidOperatorName_GettingResult_CorrectResult()
        {
            List<(string, bool)> tuples = new()
            {
                ("abc", true),
                ("a bc", false),
                (" ", false),
                ("\t", false),
                ("(", false),
                (")", false),
                (",", false),
                (".", false),
                ("((", false),
                ("))", false),
                (",,", false),
                ("..", false),
                ("++", false),
                ("-", true)
            };
            foreach ((string, bool) tuple in tuples)
            {
                Assert.AreEqual(MathOperator.IsValidOperatorName(tuple.Item1), tuple.Item2);
            }
        }

        [TestMethod]
        public void IsValidOperatorName_ArgumentNull_ArgumentNullException()
        {
            TestUtilities.AssertException<ArgumentNullException>
            (
                () =>
                {
                    MathOperator.IsValidOperatorName(null);
                },
                "It should be thrown when the argument is null."
            );
        }
    }
}
