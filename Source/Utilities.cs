using System;
using System.Collections.Generic;
using System.Text;

namespace MEP
{
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

        public static bool IsWhiteSpace(this char c)
        {
            return char.IsWhiteSpace(c);
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
                throw new ArgumentException("Cannot be negative", nameof(x));
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
                throw new ArgumentException("Cannot be negative", nameof(n));
            }
            else if (r < 0)
            {
                throw new ArgumentException("Cannot be negative", nameof(r));
            }
            return n.Factorial() / (n - r).Factorial();
        }

        public static long Combination(long n, long r)
        {
            if (n < 0)
            {
                throw new ArgumentException("Cannot be negative", nameof(n));
            }
            else if (r < 0)
            {
                throw new ArgumentException("Cannot be negative", nameof(r));
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
            if (str is "(" or ")")
            {
                return true;
            }
            return false;
        }

        public static string ReplaceRange(this string str, string insert, int start, int count)
        {
            StringBuilder result = new(str);
            return result.Remove(start, count).Insert(start, insert).ToString();
        }

        public static IList<T> ReplaceRange<T>(this IList<T> list, IList<T> insert, int start, int count)
        {
            IList<T> result = list.Clone();
            for (int i = start + count - 1; i >= start; i--)
            {
                result.RemoveAt(i);
            }
            for (int index = start, i = 0; i < insert.Count; index++, i++)
            {
                result.Insert(index, insert[i]);
            }
            return result;
        }

        public static IList<T> ReplaceRange<T>(this IList<T> list, T insert, int start, int count)
        {
            return list.ReplaceRange(insert.ToSingletonList(), start, count);
        }

        public static IList<T> ToSingletonList<T>(this T obj)
        {
            return new List<T>
            {
                obj
            };
        }

        public static IList<T> GetRange<T>(this IList<T> list, int start, int count)
        {
            IList<T> result = new List<T>();
            for (int i = start; i < start + count; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }

        public static IList<T> Clone<T>(this IList<T> original)
        {
            IList<T> result = new List<T>();
            foreach (T t in original)
            {
                result.Add(t);
            }
            return result;
        }
    }
}