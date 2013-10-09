using System;
using System.Linq;
using BuildUp.Demos.TestObjectsClass;
using BuildUp.Generators;
using NUnit.Framework;

namespace BuildUp.Demos.Demos
{
    [TestFixture]
    public class ReadMeExamples
    {

        public void Headline1()
        {

            // Create generators to build sequences of values (in BuildUp, everything is a sequence)
            var codes = StringGenerator.Numbered("user-{0}");
            codes.Create(); // Gives us { "user-0", "user-1", "user-2" ... }

            // There is range of built-in simple value generators, which should support
            // most test data scenarios, offering balance between automation and customisation

            // Random sequence of Guids (note that there's an option to seed all random generators to 
            // generate a random, but deterministic sequence, so sequence is realistically random, but 
            // deterministic)
            var ids = GuidGenerator.Random(); // { random sequence of Guids, deterministic, based on optional seed parameter)
            // Fixed values
            var ages1 = Generator.Constant(21); // { 21, 21, 21 ... }
            var ages2 = IntGenerator.Random(18, 60); // { random values between 18 and 60 }
            // Incrementing / decrementing
            var scores1 = IntGenerator.Step(2, 2); // { 2, 4, 6 ... }
            // Incrementing / decrementing by random step
            var scores2 = IntGenerator.RandomStep(1, 10, 20);
                // { starting at 1 and incrementing by random value between 10 and 20 ... }
            var signUpDates = DateGenerator.Random("2013-01-01", "2013-12-31"); // { random dates within range ... }


            // Generate value using a function
            var generator2 = Generator.Create(i => "User " + i);
            var values2 = generator2.Create();
            // "User 0", "User 1", "User 2" ...

            // Generate custom objects
            var userGenerator = Generator.Create(i => new User { UserName = "user-" + i });
            
            
            // Building the values

            // Builds sequence of users
            var users = userGenerator.Create();

            // Just build one
            var user = userGenerator.First();

            // Build a few
            var fiveCustomers = userGenerator.Take(5).ToList();
        }

        [Test]
        public void Headline2_ComplexObjects()
        {
            // Combine simple value generators together to build more complex objects

            // So far we've looked at simple generators. These are are the building blocks 
            // (think LEGO pieces) that can be combined in interesting ways to create more 
            // complex objects.

            var userGenerator = from code in StringGenerator.Numbered("user-{0}")
                        from dateOfBirth in DateGenerator.Random("1970-01-01", "2000-12-31")
                        from signUpDate in DateGenerator.Random("2012-01-01", "2013-12-31")
                        from colour in Generator.Constant("grey")
                        select new User
                        {
                            UserName = code,
                            DateOfBirth = dateOfBirth,
                            SignUpDate = signUpDate,
                            FavouriteColour = colour
                        };

            var users = userGenerator.Take(3).ToList();
            // Result:
            // { 
            //   {UserName: "user-0", DateOfBirth: 1999-06-06, FavouriteColour: "grey" ...}, 
            //   {UserName: user-1, DateOfBirth: "2000-06-04", FavouriteColour: "grey" ... },
            //   ...
            // }



            // Generators can be extended and customised, so you start with some basic generators and
            // add more specialised variations

            // Create a new generator will set a property based on values from another generator
            var niceColours = Generator.Values("Pink", "Red", "Green", "Lilac").Loop(4);
            var artisticUserGenerator = userGenerator.Set(c => c.FavouriteColour, niceColours);
            // Result:
            // { 
            //   {UserName: "user-0", DateOfBirth: 1999-06-06, FavouriteColour: "Pink" ...}, 
            //   {UserName: user-1, DateOfBirth: "2000-06-04", FavouriteColour: "Red" ... },
            //   ...
            // }

            // Alternatively, you can perform custom modification logic
            var youngUserGenerator = userGenerator.Modify(u => u.DateOfBirth = new DateTime(2000, u.DateOfBirth.Month, u.DateOfBirth.Day));


        }

        [Test]
        public void Headline2_CombinationAndModificationObjects()
        {
            // Combine simple value generators together to build more complex objects

            // So far we've looked at simple generators. It's when we combine them that things start
            // to get interesting.

            var userGenerator = from code in StringGenerator.Numbered("user-{0}")
                                from dateOfBirth in DateGenerator.Random("1970-01-01", "2000-12-31")
                                from signUpDate in DateGenerator.Random("2012-01-01", "2013-12-31")
                                from colour in Generator.Values("red", "orange", "yellow", "green").Loop(4)//TODO
                                select new User
                                {
                                    UserName = code,
                                    DateOfBirth = dateOfBirth,
                                    SignUpDate = signUpDate,
                                    FavouriteColour = colour
                                };

            var users = userGenerator.Create().Take(3).ToList();
        }

        public void Headline3_Builders()
        {
            // Use the infrastructure to create your own custom Builder classes with minimal code


            
        }

      

      

        public class User
        {
            public string UserName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string FavouriteColour { get; set; }
            public DateTime SignUpDate { get; set; }

            public override string ToString()
            {
                return string.Format("UserName: {0}, DateOfBirth: {1}, FavouriteColour: {2}, SignUpDate: {3}", UserName, DateOfBirth, FavouriteColour, SignUpDate);
            }
        }
    

    

    public class Customer
    {
        public Customer(string code)
        {
            Code = code;
        }

        public string Code { get; private set; }
    }



        



    }
}