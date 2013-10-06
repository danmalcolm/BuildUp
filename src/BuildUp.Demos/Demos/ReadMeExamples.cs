using System.Linq;
using BuildUp.Generators;
using NUnit.Framework;

namespace BuildUp.Demos.Demos
{
    [TestFixture]
    public class ReadMeExamples
    {
        public void Introduction1()
        {
            // Create some simple generators
            var codes = StringGenerator.Numbered("user-{0}");
            var ages = Generator.Constant(21);
            var randomAges = IntGenerator.Random(18, 60, 5);
            var boringColours = Generator.Values("grey", "olive");

            // These are the building blocks that can be combined in
            // interesting ways to create more interesting objects
        }

        public void Introduction2()
        {
            // Combine simple generators to generate complex objects
            var userGenerator = from code in StringGenerator.Numbered("customer-{0}")
                             from age in Generator.Constant(38)
                             from colour in Generator.Constant("grey")
                             select new User
                             {
                                 UserName = code,
                                 Age = age,
                                 FavouriteColour = colour
                             };

            // Create a sequence of objects - IEnumerable<User>
            var users = userGenerator.Create();

            // Just build one
            var user = userGenerator.First();

            // Create just a few
            var fiveCustomers = userGenerator.Take(5).ToList();

            // Create new generators based on an existing generator

            // Create a new generator that combines logic with an additional action
            var adminUserGenerator = userGenerator.Modify(c => c.FavouriteColour = "sd");
            // Another 
            adminUserGenerator = userGenerator.Set(c => c.IsAdmin, true);

            // This generator will set a property based on values from another generator
            var niceColours = Generator.Values("Pink", "Red", "Green", "Lilac").Loop(4);
            var artisticUserGenerator = userGenerator.Set(c => c.FavouriteColour, niceColours);
        }

        public class User
        {
            public string UserName { get; set; }
            public int Age { get; set; }
            public string FavouriteColour { get; set; }
            public bool IsAdmin { get; set; }
        }
    }

    public class ReadMeExamples2
    {

        public void SimpleGenerators()
        {
            // Builds sequence containing the same value
            var generator1 = Generator.Constant(1);
            var values1 = generator1.Create();
            // 1, 1, 1, 1, 1 ...

            // Generates value using a function
            var generator2 = Generator.Create(i => "User " + i);
            var values2 = generator2.Create();
            // "User 0", "User 1", "User 2" ...

            // Creating complex types
            var generator3 = Generator.Create(i => new Customer("customer-" + i));
            var values3 = generator3.Create();
            // { UserName="customer-0"}, { UserName="customer-1"}, { UserName="customer-2"} ...

            // Generates value using a function
            var generator4 = Generator.Values(new[] { "Red", "Green", "Blue" });
            var values4 = generator4.Create();
            // "Red", "Green", "Blue"
        }

        public class Customer
        {
            public Customer(string code)
            {
                Code = code;
            }

            public string Code { get; private set; }
        }



        public void IntroDemo2()
        {
            // Some other built-in generators for common value types and strings
            var generator1 = IntGenerator.Incrementing(1);
            var values1 = generator1.Create(); // 1, 2, 3, 4, 5 ...

            var generator2 = IntGenerator.Incrementing(1, 2);
            var values2 = generator2.Create(); // 1, 3, 5, 7 ...

            var generator3 = StringGenerator.Numbered("User {1}");
            var values3 = generator3.Create(); // "User 1", "User 2", "User 3" ...

            var generator4 = StringGenerator.Numbered("Item {0}");
            var values4 = generator4.Create(); // "Item 0", "Item 1", "Item 2" ...

            var generator5 = GuidGenerator.Sequence(1);
            var values5 = generator5.Create();
        }



    }
}