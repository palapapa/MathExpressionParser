using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MathExpressionParser;

internal static class Utilities
{
    public static bool IsDigit(this char c)
    {
        return char.IsDigit(c);
    }

    public static bool IsLetter(this char c)
    {
        return char.IsLetter(c);
    }

    public static bool IsWhitespace(this char c)
    {
        return char.IsWhiteSpace(c);
    }

    public static bool IsWhitespace(this string s)
    {
        if (s is null)
        {
            throw new ArgumentNullException(nameof(s));
        }
        return string.IsNullOrWhiteSpace(s);
    }

    public static double ToDouble(this string s)
    {
        return double.Parse(s);
    }

    public static double ToThePowerOf(this double @base, double exponent)
    {
        return Math.Pow(@base, exponent);
    }

    public static double Deg2Rad(this double degree)
    {
        return degree * Math.PI / 180;
    }

    public static double Rad2Deg(this double radian)
    {
        return radian * 180 / Math.PI;
    }

    public static long Factorial(this long x)
    {
        if (x < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }
        long result = 1;
        for (int i = 2; i <= x; i++)
        {
            result *= i;
        }
        return result;
    }

    public static long Permutation(long n, long r)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n));
        }
        if (r < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(r));
        }
        if (r > n)
        {
            throw new ArgumentException($"{nameof(r)} cannot be larger than {nameof(n)}");
        }
        return n.Factorial() / (n - r).Factorial();
    }

    public static long Combination(long n, long r)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n));
        }
        if (r < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(r));
        }
        if (r > n)
        {
            throw new ArgumentException($"{nameof(r)} cannot be larger than {nameof(n)}");
        }
        return n.Factorial() / (r.Factorial() * (n - r).Factorial());
    }

    public static double Log(double x, double @base)
    {
        return Math.Log10(x) / Math.Log10(@base);
    }

    public static bool IsDouble(this string str)
    {
        try
        {
            double.Parse(str);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool IsParenthesis(this string str)
    {
        if (str is null)
        {
            throw new ArgumentNullException($"{nameof(str)}");
        }
        if (str is "(" or ")")
        {
            return true;
        }
        return false;
    }

    public static string ReplaceRange(this string str, string insert, int start, int count)
    {
        if (str is null)
        {
            throw new ArgumentNullException($"{nameof(str)}");
        }
        if (insert is null)
        {
            throw new ArgumentNullException($"{nameof(insert)}");
        }
        StringBuilder result = new(str);
        return result.Remove(start, count).Insert(start, insert).ToString();
    }

    public static List<T> ReplaceRange<T>(this List<T> list, List<T> insert, int start, int count)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }
        if (start < 0 || start >= list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }
        if (count < 0 || start + count > list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }
        List<T> result = list.ToList();
        result.RemoveRange(start, count);
        result.InsertRange(start, insert);
        return result;
    }

    public static List<T> ReplaceRange<T>(this List<T> list, T insert, int start, int count)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }
        if (start < 0 || start >= list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }
        if (count < 0 || start + count > list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }
        return list.ReplaceRange(insert.ToSingletonList(), start, count);
    }

    public static List<T> ToSingletonList<T>(this T obj)
    {
        return new List<T>
        {
            obj
        };
    }

    public static T BoundElememtAt<T>(this List<T> list, int index)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }
        try
        {
            return list[index];
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    public static char BoundElememtAt(this string str, int index)
    {
        if (str is null)
        {
            throw new ArgumentNullException(nameof(str));
        }
        try
        {
            return str[index];
        }
        catch (IndexOutOfRangeException)
        {
            return default;
        }
    }
}
