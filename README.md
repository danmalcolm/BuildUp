BuildUp
=======

Introduction
------------

A library to ease the pain of creating objects for automated tests or demo data.



How it Works
------------

BuildUp provides some infrastructure supporting that make it very easy to create test object builders:

- A range of utility functionality
- Builder infrastructure
- Easily build collections of objects (in BuildUp, everything is a sequence)

On a simple project, you might be happy with a simple ObjectBuilder, like FactoryGirl:

    TODO

Test Data Builder for more complex project:
    
Here's what you might come up with once you get the hang of it:

    public class BookingBuilder : BuilderBase<Booking, BookingBuilder>
    {
        private IGenerator<Hotel> hotels = new HotelBuilder().Freeze();
        private IGenerator<Customer> customers = new CustomerBuilder();
        private IGenerator<DateTime> startDates = DateGenerator.Step(new DateTime(2012, 1, 1), TimeSpan.FromDays(1));

        protected override IGenerator<Booking> GetGenerator()
        {
            return from hotel in hotels
                   from customer in customers
                   from date in startDates
                   select new Booking(hotel, customer, date);
        }

        public BookingBuilder AtHotel(IGenerator<Hotel> hotels)
        {
            return Copy(me => me.hotels = hotels);
        }

        public BookingBuilder WithCustomer(IGenerator<Customer> customers)
        {
            return Copy(me => me.customers = customers);
        }

        public BookingBuilder StartingOn(IGenerator<DateTime> startDates)
        {
            return Copy(me => me.startDates = startDates);
        }
    }

    
Generators are the building blocks of BuildUp.

Create generators to build sequences of values (in BuildUp, everything is a sequence):

    var codes = StringGenerator.Numbered("user-{0}");
    codes.Create(); // Gives us { "user-0", "user-1", "user-2" ... }

There is range of built-in simple value generators, which should support most test data scenarios, offering balance between automation and customisation:

Random sequences of Guids (note that there's an option to seed all random generators to generate a random, but deterministic sequence, so sequence is realistically random, but deterministic).

    var ids = GuidGenerator.Random(); // { random sequence of Guids, deterministic, based on optional seed parameter)

Fixed values

    var ages1 = Generator.Constant(21); // { 21, 21, 21 ... }
    var ages2 = IntGenerator.Random(18, 60); // { random values between 18 and 60 }

Incrementing / decrementing:

    var scores1 = IntGenerator.Step(2, 2); // { 2, 4, 6 ... }

Incrementing / decrementing by random step (starting at 1 and incrementing by a random value between 10 and 20)

    var scores2 = IntGenerator.RandomStep(1, 10, 20);

Random dates within a range:
    
    var signUpDates = DateGenerator.Random("2013-01-01", "2013-12-31"); // { random dates within range ... }

Generate values using a function:

    var generator2 = Generator.Create(i => "User " + i);
    var values2 = generator2.Create();
    // "User 0", "User 1", "User 2" ...

Generate custom objects:

    var userGenerator = Generator.Create(i => new User { UserName = "user-" + i });

Building the values:

    var users = userGenerator.Create(); // Builds infinite sequence of users
    var user = userGenerator.First(); // Just build one
    var users2 = userGenerator.Take(5).ToList(); // Build a few
            
See the [Wiki Introduction Page](https://github.com/danmalcolm/BuildUp/wiki/buildup-generators) for background.

Development
------------

Coding conventions

Setting up XUnit / R#




