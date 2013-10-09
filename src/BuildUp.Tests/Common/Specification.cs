using System;

namespace BuildUp.Tests.Common
{
    /// <summary>
    /// Base class for tests
    /// </summary>
    public class Specification 
    {
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