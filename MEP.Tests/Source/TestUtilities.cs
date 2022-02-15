using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MEP.Tests;

internal static class TestUtilities
{
    public static void AssertException<Exception>(Action action, string noExceptionMessage = "", string wrongExceptionMessage = "") where Exception : System.Exception
    {
        try
        {
            action();
            Assert.Fail($"{typeof(Exception)} was not thrown. {noExceptionMessage}");
        }
        catch (Exception)
        {

        }
        catch (System.Exception e) when (e is not (Exception or AssertFailedException))
        {
            Assert.Fail($"{e.GetType()} was thrown instead of {typeof(Exception)}. {wrongExceptionMessage}\nMessage: {e.Message}");
        }
    }

    public static void AssertException<Exception>(Action action, string message = "") where Exception : System.Exception
    {
        AssertException<Exception>(action, message, message);
    }
}
