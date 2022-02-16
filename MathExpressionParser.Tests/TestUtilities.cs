using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathExpressionParser.Tests;

internal static class TestUtilities
{
    public static void AssertException<Exception>(Action action, string message = "") where Exception : System.Exception
    {
        try
        {
            action();
            Assert.Fail($"{typeof(Exception)} was not thrown. {message}");
        }
        catch (Exception)
        {

        }
        catch (System.Exception e) when (e is not (Exception or AssertFailedException))
        {
            Assert.Fail($"{e.GetType()} was thrown instead of {typeof(Exception)}. {message}\nMessage of {e.GetType()}: {e.Message}");
        }
    }

    public static void AssertException<Exception>(Action action, Predicate<Exception> predicate, string message = "") where Exception : System.Exception
    {
        try
        {
            action();
            Assert.Fail($"{typeof(Exception)} was not thrown. {message}");
        }
        catch (Exception e)
        {
            if (!predicate(e))
            {
                Assert.Fail($"{typeof(Exception)} was thrown but it didn't satisify the {nameof(predicate)}. {message}\nMessage of {typeof(Exception)}: {e.Message}");
            }
        }
        catch (System.Exception e) when (e is not (Exception or AssertFailedException))
        {
            Assert.Fail($"{e.GetType()} was thrown instead of {typeof(Exception)}. {message}\nMessage of {e.GetType()}: {e.Message}");
        }
    }
}
