BuildUp - A test object builder micro-framework for .Net
======================================================

Introduction
------------

BuildUp provides an infrastructure for generating objects for unit / integration tests or sample data:

* Generate objects with sensible default values using a concise declarative API
* Use a range of built-in mechanisms for generating property values (date and number sequences etc.)
* Extend object generators with custom set-up behaviours
* First-class support for generating collections of objects - in BuildUp, everything is a sequence
* Infrastructure for creating extremely concise [Test Data Builders](http://www.natpryce.com/articles/000714.html)
* Designed with "real" behavioural domain models in mind

How it Works
------------

You define sequences of values using [Generators](wiki/buildup-generators) like this:

    var codes = StringGenerator.Numbered("user-{0}"); // { "user-0", "user-1" ... } - use {1} for 1-based index of value
    var ids = GuidGenerator.Random(); // { random sequence of Guids } - use optional seed parameter for deterministic values
    var ages1 = Generator.Constant(21); // { 21, 21, 21 ... }
    var ages2 = IntGenerator.Random(18, 60); // { random values between 18 and 60 }

Then, you combine simple generators to create more complex objects:

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

The `Build` method builds the objects:

	var users = userGenerator.Build();
    // Returns IEnumerable<User>:
    // { 
    //   {UserName: "user-0", DateOfBirth: 1999-06-06, FavouriteColour: "grey" ...}, 
    //   {UserName: user-1, DateOfBirth: "2000-06-04", FavouriteColour: "grey" ... },
    //   ...
    // }
	
Or you can use some other convenience methods:

	var users = userGenerator.Take(3);
	var user = userGenerator.First();

Generators can be customised in all sorts of ways, allowing you to extend default object setup with more specialised modifications:

    // Create a new generator will set a property based on values from another generator
    var niceColours = Generator.Values("Pink", "Red", "Green", "Lilac").Loop(4);
    var artisticUserGenerator = userGenerator.Set(c => c.FavouriteColour, niceColours);
    // Result:
    // { 
    //   {UserName: "user-0", DateOfBirth: 1999-06-06, FavouriteColour: "Pink" ...}, 
    //   {UserName: user-1, DateOfBirth: "2000-06-04", FavouriteColour: "Red" ... },
    //   ...
    // }

Note that generators are immutable - all extension mechanisms like the *Set* extension method above create new generators, leaving the originals unmodified.

You might favour writing your own extension methods to define reusable setup behaviours:

    public static class UserExtensions
    {
        public static IGenerator<User> LockedOut(this IGenerator<User> generator)
        {
            return generator.Modify(u => {
                // add invalid log-in attempts etc
            });
        }
    }

    // snip
    var user = userGenerator.LockedOut().First();
    
See [Generators](wiki/buildup-generators) for full background on generators.

When managing a large or complex "hide your secrets, no setters, don't mess" domain model, you can use BuildUp to create [Test Data Builders](http://c2.com/cgi/wiki?TestDataBuilder). Here's what you might come up with once you get the hang of it:

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


What Next?
----------

See [the wiki](wiki) for full details of BuildUp and its components. 


Upcoming Features?
------------------

The TODO list includes:

- BuildUp.TestData subproject with realistic demo data, per-locale names, addresses, phone numbers etc
- Better support for building collection properties


Can You Help?
-------------

BuildUp has been used in private on a number of projects and I'm pleased with it so far. 

Feedback at this stage would be super-great, in particular:

* Suggestions for API improvements, no matter how radical!
* Must-have features that you'd need to use BuildUp on your own project

Add an issue, PR or send me a message.

