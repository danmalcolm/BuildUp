using BuildUp.ValueGenerators;
using NUnit.Framework;
using System.Linq;

namespace BuildUp.Tests.Demos
{
    [TestFixture]
    public class ReadMeExamples
    {
        public void Introduction()
        {
            // Create some simple generators
            var codes = StringGenerator.Numbered("customer-{0}");
            var ages = Generator.Constant(38);
            var boringColours = Generator.Constant("grey");

            // Combine simple generators to generate complex objects
            var generator1 = from code in codes
                            from age in ages
                            from colour in boringColours
                            select new Customer
                            {
                                Code = code,
                                Age = age,
                                FavouriteColour = colour
                            };

            // Build a sequence of objects (IEnumerable<Customer>)
            var customers = generator1.Build();

            // Just build one
            var customer = generator1.First();
            
            // Build just a few
            var fiveCustomers = generator1.Take(5).ToList();

            // Create new generators based on an existing generator

            // Create a new generator that combines logic with an additional action
            var generator2 = generator1.Select(c => c.Age -= 10);

            // This generator will set a property based on values from another generator
            var niceColours = Generator.FromSequence("Pink", "Red", "Green", "Lilac").Loop(4);
            var generator3 = generator1.Set(c => c.FavouriteColour, niceColours);
        }

        public class Customer
        {
            public string Code { get; set; }
            public int Age { get; set; }
            public string FavouriteColour { get; set; }
        }
    }

    public class ReadMeExamples2{

        public void SimpleGenerators()
        {
            // Builds sequence containing the same value
            var generator1 = Generator.Constant(1);
            var values1 = generator1.Build(); 
            // 1, 1, 1, 1, 1 ...

            // Generates value using a function
            var generator2 = Generator.Create(i => "Customer " + i);
            var values2 = generator2.Build(); 
            // "Customer 0", "Customer 1", "Customer 2" ...

            // Creating complex types
            var generator3 = Generator.Create(i => new Customer("customer-" + i));
            var values3 = generator3.Build();
            // { Code="customer-0"}, { Code="customer-1"}, { Code="customer-2"} ...

            // Generates value using a function
            var generator4 = Generator.FromSequence(new [] { "Red", "Green", "Blue" });
            var values4 = generator4.Build();
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
            var values1 = generator1.Build(); // 1, 2, 3, 4, 5 ...

            var generator2 = IntGenerator.Incrementing(1, 2);
            var values2 = generator2.Build(); // 1, 3, 5, 7 ...

            var generator3 = StringGenerator.Numbered("Customer {1}");
            var values3 = generator3.Build(); // "Customer 1", "Customer 2", "Customer 3" ...

            var generator4 = StringGenerator.Numbered("Item {0}");
            var values4 = generator4.Build(); // "Item 0", "Item 1", "Item 2" ...

            var generator5 = GuidGenerator.Incrementing(1);
            var values5 = generator5.Build();
        }



    }
}