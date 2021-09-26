using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEP.Tests
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void IsDigit_GettingResult_CorrectResult()
        {
            Assert.AreEqual('a'.IsDigit(), false);
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(i.ToString()[0].IsDigit(), true);
            }
        }

        [TestMethod]
        public void IsLetter_GettingResult_CorrectResult()
        {
            Assert.AreEqual('0'.IsLetter(), false);
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Assert.AreEqual(c.IsLetter(), true, c.ToString());
            }
            for (char c = 'a'; c <= 'z'; c++)
            {
                Assert.AreEqual(c.IsLetter(), true, c.ToString());
            }
        }

        [TestMethod]
        public void IsWhiteSpace_GettingResult_CorrectResult()
        {
            Assert.AreEqual('\t'.IsWhiteSpace(), true);
            Assert.AreEqual(' '.IsWhiteSpace(), true);
            Assert.AreEqual('a'.IsWhiteSpace(), false);
        }

        [TestMethod]
        public void ToDouble_GettingResult_CorrectResult()
        {
            Assert.AreEqual("1.5".ToDouble(), 1.5);
            Assert.AreEqual("-1.5".ToDouble(), -1.5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "FormatException should be thrown when the provided string is not a double.", AllowDerivedTypes = false)]
        public void ToDouble_InvalidFormat_FormatException()
        {
            "abc".ToDouble();
        }

        [TestMethod]
        public void ToThePowerOf_GettingResult_CorrectResult()
        {
            Assert.AreEqual(2d.ToThePowerOf(10), 1024);
        }

        [TestMethod]
        public void Deg2Rad_GettingResult_CorrectResult()
        {
            Assert.AreEqual(360d.Deg2Rad(), Math.PI * 2);
        }

        [TestMethod]
        public void Rad2Deg_GettingResult_CorrectResult()
        {
            Assert.AreEqual((Math.PI * 2).Rad2Deg(), 360);
        }

        [TestMethod]
        public void Factorial_GettingResult_CorrectResult()
        {
            Assert.AreEqual(5L.Factorial(), 120);
            Assert.AreEqual(0L.Factorial(), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when the argument is negative.", AllowDerivedTypes = false)]
        public void Factorial_NegativeArgument_ArgumentException()
        {
            (-1L).Factorial();
        }

        [TestMethod]
        public void Permutation_GettingResult_CorrectResult()
        {
            Assert.AreEqual(Utilities.Permutation(5, 2), 20);
            Assert.AreEqual(Utilities.Permutation(5, 0), 1);
        }

        [TestMethod]
        public void Permutation_NegativeArguments_ArgumentException()
        {
            List<KeyValuePair<int, int>> pairs = new()
            {
                new(-5, 2),
                new(5, -2),
                new(-5, -2)
            };
            foreach (KeyValuePair<int, int> pair in pairs)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        Utilities.Permutation(pair.Key, pair.Value);
                    },
                    "It should be thrown when either or both of the arguments are negative."
                );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when the second argument is larger than the first argument.", AllowDerivedTypes = false)]
        public void Permutation_SecondArgumentLargerThanFirstArgument_ArgumentException()
        {
            Utilities.Permutation(5, 6);
        }

        [TestMethod]
        public void Combination_GettingResult_CorrectResult()
        {
            Assert.AreEqual(Utilities.Combination(5, 2), 10);
            Assert.AreEqual(Utilities.Combination(5, 0), 1);
        }

        [TestMethod]
        public void Combination_NegativeArguments_ArgumentException()
        {
            List<KeyValuePair<int, int>> pairs = new()
            {
                new(-5, 2),
                new(5, -2),
                new(-5, -2)
            };
            foreach (KeyValuePair<int, int> pair in pairs)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        Utilities.Combination(pair.Key, pair.Value);
                    },
                    "It should be thrown when either or both of the arguments are negative."
                );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ArgumentException should be thrown when the second argument is larger than the first argument.", AllowDerivedTypes = false)]
        public void Combination_SecondArgumentLargerThanFirstArgument_ArgumentException()
        {
            Utilities.Combination(5, 6);
        }

        [TestMethod]
        public void Log_GettingResult_CorrectResult()
        {
            List<KeyValuePair<KeyValuePair<double, double>, double>> pairs = new()
            {
                new(new(100, 10), 2),
                new(new(1, 10), 0),
                new(new(100, 0.1), -2),
                new(new(0.1, 10), -1),
                new(new(100, 1), double.PositiveInfinity),
                new(new(100, 0), 0),
                new(new(0, 10), double.NegativeInfinity),
                new(new(0, 0), double.NaN),
                new(new(-100, 10), double.NaN),
                new(new(100, -10), double.NaN),
                new(new(-100, -10), double.NaN),
                new(new(double.NaN, 10), double.NaN),
                new(new(100, double.NaN), double.NaN),
                new(new(double.NaN, double.NaN), double.NaN),
                new(new(double.PositiveInfinity, 10), double.PositiveInfinity),
                new(new(100, double.PositiveInfinity), 0),
                new(new(double.PositiveInfinity, double.PositiveInfinity), double.NaN)
            };
            foreach (KeyValuePair<KeyValuePair<double, double>, double> pair in pairs)
            {
                Assert.AreEqual(Utilities.Log(pair.Key.Key, pair.Key.Value), pair.Value, $"{pair.Key.Key}, {pair.Key.Value}");
            }
        }

        [TestMethod]
        public void IsDouble_GettingResult_CorrectResult()
        {
            IReadOnlyDictionary<string, bool> pairs = new Dictionary<string, bool>()
            {
                { "69.420", true },
                { "1E5", true },
                { "-00.0", true },
                { "1e5", true },
                { "1", true },
                { "abc", false },
                { "", false },
                { "  ", false },
                { "\t", false },
                { "\n", false },
                { "1.0O", false },
                { new Random().NextDouble().ToString(), true }
            };
            foreach (KeyValuePair<string, bool> pair in pairs)
            {
                Assert.AreEqual(pair.Key.IsDouble(), pair.Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
        public void IsDouble_ArgumentNull_ArgumentNullException()
        {
            string thisIsAVeryNullStringThatIsVeryNullItIsSoNullThatItCausesAnArgumentNullException = null;
            thisIsAVeryNullStringThatIsVeryNullItIsSoNullThatItCausesAnArgumentNullException.IsDouble();
        }

        [TestMethod]
        public void IsParenthesis_GettingResult_CorrectResult()
        {
            IReadOnlyDictionary<string, bool> pairs = new Dictionary<string, bool>()
            {
                { "(", true },
                { ")", true },
                { "", false },
                { "  ", false },
                { "\t", false },
                { "\n", false },
                { "((", false },
                { "))", false }
            };
            foreach (KeyValuePair<string, bool> pair in pairs)
            {
                Assert.AreEqual(pair.Key.IsParenthesis(), pair.Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
        public void IsParenthesis_ArgumentNull_ArgumentNullException()
        {
            string thisIsAVeryNullStringThatIsVeryNullItIsSoNullThatItCausesAnArgumentNullException = null;
            thisIsAVeryNullStringThatIsVeryNullItIsSoNullThatItCausesAnArgumentNullException.IsParenthesis();
        }

        [TestMethod]
        public void ReplaceRange_GettingResult_CorrectResult()
        {
            string str = "test string";
            Assert.AreEqual(str.ReplaceRange("replaced", 0, 4), "replaced string");
            Assert.AreEqual(str.ReplaceRange("", 0, 4), " string");
            Assert.AreEqual(str.ReplaceRange("replaced", 0, 0), "replacedtest string");
            List<string> replacee = new() { "0", "1", "2", "3", "4" };
            List<string> replacer = new() { "5", "6" };
            List<string> empty = new();
            List<string> nullList = new() { null, null };
            IReadOnlyDictionary<IList<string>, IList<string>> pairs = new Dictionary<IList<string>, IList<string>>
            {
                { replacee.ReplaceRange(replacer, 0, 2), new List<string> { "5", "6", "2", "3", "4" } },
                { replacee.ReplaceRange(empty, 0, 2), new List<string> { "2", "3", "4" } },
                { replacee.ReplaceRange(nullList, 0, 2), new List<string> { null, null, "2", "3", "4" } },
                { replacee.ReplaceRange("5", 0, 2), new List<string> { "5", "2", "3", "4" } },
                { replacee.ReplaceRange("5", 0, 0), new List<string> { "5", "0", "1", "2", "3", "4" } },
                { replacee.ReplaceRange((string)null, 0, 2), new List<string> { null, "2", "3", "4" } }
            };
            foreach (KeyValuePair<IList<string>, IList<string>> pair in pairs)
            {
                if (!pair.Key.SequenceEqual(pair.Value))
                {
                    StringBuilder key = new(), value = new();
                    foreach (string s in pair.Key)
                    {
                        key.Append($"\"{s}\", ");
                    }
                    foreach (string s in pair.Value)
                    {
                        value.Append($"\"{s}\", ");
                    }
                    Assert.Fail($"Key list = {key.ToString().Trim()} Value list = {value.ToString().Trim()}");
                }
            }
        }

        [TestMethod]
        public void ReplaceRangeStringStringIntInt_ArgumentNull_ArgumentNullException()
        {
            List<(string, string, int, int)> arguments = new()
            {
                new(null, "", 0, 0),
                new("", null, 0, 0),
                new(null, null, 0, 0)
            };
            foreach ((string, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when some of the string arguments are null."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeStringStringIntInt_NegativeArgument_ArgumentException()
        {
            List<(string, string, int, int)> arguments = new()
            {
                new("", "", -1, 0),
                new("", "", 0, -1),
                new("", "", -1, -1)
            };
            foreach ((string, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are negative."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeStringStringIntInt_OutOfRange_ArgumentOutOfRangeException()
        {
            string test = "test";
            List<(string, string, int, int)> arguments = new()
            {
                new(test, test, 0, 5),
                new(test, test, 4, 1)
            };
            foreach ((string, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentOutOfRangeException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are out of range."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListIListIntInt_ArgumentNull_ArgumentNullException()
        {
            List<(List<string>, List<string>, int, int)> arguments = new()
            {
                new(null, new(), 0, 0),
                new(null, null, 0, 0)
            };
            foreach ((IList<string>, IList<string>, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the some of the IList arguments are null."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListIListIntInt_NegativeArgument_ArgumentException()
        {
            List<(List<string>, List<string>, int, int)> arguments = new()
            {
                new(new(), new(), -1, 0),
                new(new(), new(), 0, -1),
                new(new(), new(), -1, -1)
            };
            foreach ((IList<string>, IList<string>, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are negative."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListIListIntInt_OutOfRange_ArgumentOutOfRangeException()
        {
            List<string> list = new() { "0", "1", "2", "3", "4" };
            List<(IList<string>, IList<string>, int, int)> arguments = new()
            {
                (list, list, 0, 6),
                (list, list, 5, 1)
            };
            foreach ((IList<string>, IList<string>, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentOutOfRangeException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are out of range."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListTIntInt_ArgumentNull_ArgumentNullException()
        {
            List<(List<string>, string, int, int)> arguments = new()
            {
                new(null, string.Empty, 0, 0),
                new(null, null, 0, 0)
            };
            foreach ((IList<string>, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentNullException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the first argument is null."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListTIntInt_NegativeArgument_ArgumentException()
        {
            List<(List<string>, string, int, int)> arguments = new()
            {
                new(new(), string.Empty, -1, 0),
                new(new(), string.Empty, 0, -1),
                new(new(), string.Empty, -1, -1)
            };
            foreach ((IList<string>, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are negative."
                );
            }
        }

        [TestMethod]
        public void ReplaceRangeIListTIntInt_OutOfRange_ArgumentOutOfRangeException()
        {
            List<string> list = new() { "0", "1", "2", "3", "4" };
            string str = "5";
            List<(IList<string>, string, int, int)> arguments = new()
            {
                (list, str, 0, 6),
                (list, str, 5, 1)
            };
            foreach ((IList<string>, string, int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentOutOfRangeException>
                (
                    () =>
                    {
                        tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4);
                    },
                    "It should be thrown when the int arguments are out of range."
                );
            }
        }

        [TestMethod]
        public void ToList_GettingResult_CorrectResult()
        {
            int i = new Random().Next();
            if (!i.ToSingletonList().SequenceEqual(new List<int>() { i }))
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void GetRange_GettingResult_CorrectResult()
        {
            IList<string> list = new List<string>() { "0", "1", "2", "3", "4" };
            if
            (
                !list.GetRange(1, 2).SequenceEqual(new List<string>() { "1", "2" }) ||
                !list.GetRange(1, 0).SequenceEqual(new List<string>())
            )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
        public void GetRange_ArgumentNull_ArgumentNullException()
        {
            ((IList<string>)null).GetRange(0, 0);
        }

        [TestMethod]
        public void GetRange_NegativeArgument_ArgumentException()
        {
            IList<int> list = new List<int>() { 0, 1, 2, 3, 4 };
            List<(int, int)> arguments = new()
            {
                (-1, 0),
                (0, -1),
                (-1, -1)
            };
            foreach ((int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentException>
                (
                    () =>
                    {
                        list.GetRange(tuple.Item1, tuple.Item2);
                    },
                    "It should be thrown when some of the arguments are negative."
                );
            }
        }

        [TestMethod]
        public void GetRange_OutOfRange_ArgumentOutOfRangeException()
        {
            IList<int> list = new List<int>() { 0, 1, 2, 3, 4 };
            List<(int, int)> arguments = new()
            {
                (5, 2),
                (4, 2),
                (3, 3)
            };
            foreach ((int, int) tuple in arguments)
            {
                TestUtilities.AssertException<ArgumentOutOfRangeException>
                (
                    () =>
                    {
                        list.GetRange(tuple.Item1, tuple.Item2);
                    },
                    "It should be thrown when some of the arguments are out of range."
                );
            }
        }

        [TestMethod]
        public void Clone_GettingResult_CorrectResult()
        {
            IList<int> original = new List<int>() { 0, 1, 2, 3, 4 };
            if (!original.Clone().SequenceEqual(original))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException should be thrown when the input is null.", AllowDerivedTypes = false)]
        public void Clone_ArgumentNull_ArgumentNullException()
        {
            ((IList<int>)null).Clone();
        }
    }
}