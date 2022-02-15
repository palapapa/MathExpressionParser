using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEP.Tests;

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
    public void IsWhitespaceChar_GettingResult_CorrectResult()
    {
        Assert.AreEqual('\t'.IsWhitespace(), true);
        Assert.AreEqual(' '.IsWhitespace(), true);
        Assert.AreEqual('a'.IsWhitespace(), false);
    }

    [TestMethod]
    public void IsWhitespaceString_GettingResult_CorrectResult()
    {
        Assert.AreEqual("      \t    ".IsWhitespace(), true);
        Assert.AreEqual("\n\t\t\t     \t\t\n".IsWhitespace(), true);
        Assert.AreEqual("a".IsWhitespace(), false);
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
    [ExpectedException(typeof(ArgumentOutOfRangeException), $"{nameof(ArgumentOutOfRangeException)} should be thrown when the argument is negative.", AllowDerivedTypes = false)]
    public void Factorial_NegativeArgument_ArgumentOutOfRangeException()
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
    public void Permutation_NegativeArguments_ArgumentOutOfRangeException()
    {
        List<KeyValuePair<int, int>> pairs = new()
        {
            new(-5, 2),
            new(5, -2),
            new(-5, -2)
        };
        foreach (KeyValuePair<int, int> pair in pairs)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => Utilities.Permutation(pair.Key, pair.Value),
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
    public void Combination_NegativeArguments_ArgumentOutOfRangeException()
    {
        List<KeyValuePair<int, int>> pairs = new()
        {
            new(-5, 2),
            new(5, -2),
            new(-5, -2)
        };
        foreach (KeyValuePair<int, int> pair in pairs)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => Utilities.Combination(pair.Key, pair.Value),
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
        Dictionary<List<string>, List<string>> pairs = new()
        {
            { replacee.ReplaceRange(replacer, 0, 2), new List<string> { "5", "6", "2", "3", "4" } },
            { replacee.ReplaceRange(empty, 0, 2), new List<string> { "2", "3", "4" } },
            { replacee.ReplaceRange(nullList, 0, 2), new List<string> { null, null, "2", "3", "4" } },
            { replacee.ReplaceRange("5", 0, 2), new List<string> { "5", "2", "3", "4" } },
            { replacee.ReplaceRange("5", 0, 0), new List<string> { "5", "0", "1", "2", "3", "4" } },
            { replacee.ReplaceRange((string)null, 0, 2), new List<string> { null, "2", "3", "4" } }
        };
        foreach (KeyValuePair<List<string>, List<string>> pair in pairs)
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
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when some of the string arguments are null."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeStringStringIntInt_NegativeArgument_ArgumentOutOfRangeException()
    {
        List<(string, string, int, int)> arguments = new()
        {
            new("", "", -1, 0),
            new("", "", 0, -1),
            new("", "", -1, -1)
        };
        foreach ((string, string, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
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
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the int arguments are out of range."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListListIntInt_ArgumentNull_ArgumentNullException()
    {
        List<(List<string>, List<string>, int, int)> arguments = new()
        {
            new(null, new(), 0, 0),
            new(null, null, 0, 0)
        };
        foreach ((List<string>, List<string>, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentNullException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the some of the List arguments are null."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListListIntInt_NegativeArgument_ArgumentOutOfRangeException()
    {
        List<(List<string>, List<string>, int, int)> arguments = new()
        {
            new(new(), new(), -1, 0),
            new(new(), new(), 0, -1),
            new(new(), new(), -1, -1)
        };
        foreach ((List<string>, List<string>, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the int arguments are negative."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListListIntInt_OutOfRange_ArgumentOutOfRangeException()
    {
        List<string> list = new() { "0", "1", "2", "3", "4" };
        List<(List<string>, List<string>, int, int)> arguments = new()
        {
            (list, list, 0, 6),
            (list, list, 5, 1)
        };
        foreach ((List<string>, List<string>, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the int arguments are out of range."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListTIntInt_ArgumentNull_ArgumentNullException()
    {
        List<(List<string>, string, int, int)> arguments = new()
        {
            new(null, string.Empty, 0, 0),
            new(null, null, 0, 0)
        };
        foreach ((List<string>, string, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentNullException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the first argument is null."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListTIntInt_NegativeArgument_ArgumentOutOfRangeException()
    {
        List<(List<string>, string, int, int)> arguments = new()
        {
            new(new(), string.Empty, -1, 0),
            new(new(), string.Empty, 0, -1),
            new(new(), string.Empty, -1, -1)
        };
        foreach ((List<string>, string, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the int arguments are negative."
            );
        }
    }

    [TestMethod]
    public void ReplaceRangeListTIntInt_OutOfRange_ArgumentOutOfRangeException()
    {
        List<string> list = new() { "0", "1", "2", "3", "4" };
        string str = "5";
        List<(List<string>, string, int, int)> arguments = new()
        {
            (list, str, 0, 6),
            (list, str, 5, 1)
        };
        foreach ((List<string>, string, int, int) tuple in arguments)
        {
            TestUtilities.AssertException<ArgumentOutOfRangeException>
            (
                () => tuple.Item1.ReplaceRange(tuple.Item2, tuple.Item3, tuple.Item4),
                "It should be thrown when the int arguments are out of range."
            );
        }
    }

    [TestMethod]
    public void ToSingletonList_GettingResult_CorrectResult()
    {
        int i = new Random().Next();
        if (!i.ToSingletonList().SequenceEqual(new List<int>() { i }))
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void BoundElememtAt_GettingResult_CorrectResult()
    {
        List<string> tokens = "0 1 2 3 4 5 6 7 8 9".Split(' ').ToList();
        List<(int, string)> arguments = new()
        {
            (1, "1"),
            (7, "7"),
            (69, null),
            (-1, null)
        };
        foreach ((int, string) argument in arguments)
        {
            Assert.AreEqual(argument.Item2, tokens.BoundElememtAt(argument.Item1));
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), $"{nameof(ArgumentNullException)} should be thrown when the tokens argument is null.", AllowDerivedTypes = false)]
    public void BoundElememtAt_ArgumentNull_ArgumentNullException()
    {
        ((List<string>)null).BoundElememtAt(0);
    }
}
