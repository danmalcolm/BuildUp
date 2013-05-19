using System.Linq;
using NUnit.Framework;
using BuildUp.Generators;

namespace BuildUp.Demos.TestObjectsClass
{
    /// <summary>
    /// Example of using a TestObjects class with static members as a location for accessing
    /// test data. Works well for smaller project with just a few classes.
    /// </summary>
    [TestFixture]
    public class ReadMe
    {
        public void GettingUsers()
        {
            var users = TestObjects.Users.Take(2).ToList();
            var adminUsers = TestObjects.Users.Set(x => x.IsAdmin, true).First();
        }
         
    }
}