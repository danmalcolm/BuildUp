using System;
using NUnit.Framework;

namespace BuildUp.Tests.Common
{
    /// <summary>
    /// Base class for tests
    /// </summary>
    public class Specification 
    {
        protected void ExpectException(Action action, Action<Exception> validateException = null)
        {
            var exception = Catch(action);
            if(exception == null)
            {
                Assert.Fail("Exception not thrown");
            }
            if(validateException != null)
            {
                validateException(exception);
            }
        }

        protected Exception Catch(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                return exception;
            }
            return null;
        }
    }
}