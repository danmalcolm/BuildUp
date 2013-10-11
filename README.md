BuildUp
=======

Introduction
------------

BuildUp is a test object builder micro-framework for .Net. It provides a useful infrastructure for creating objects suitable for writing automated tests or setting up sample data.

BuildUp provides just enough code to help you:

* Set up creation of objects with sensible default values using an intuitive concise API
* Use a range of built-in mechanisms for generating values (emails, dates, number sequences etc)
* Extend and customise the objects you build to allow variations
* First-class support for generating collections of objects - in BuildUp, everything is a sequence
* Infrastructure for creating extremely concise (Test Data Builders)[http://www.natpryce.com/articles/000714.html]
* Designed for generating real domain objects without setters and stuff, immutable value objects etc.

How it Works
------------

You define sequences of values used to initialise properties using [Generators](wiki/buildup-generators) like this:

    var codes = StringGenerator.Numbered("user-{0}"); // { "user-0", "user-1" ... } - use {1} for 1-based index of value
    var ids = GuidGenerator.Random(); // { random sequence of Guids } - use optional seed parameter for deterministic values
    var ages1 = Generator.Constant(21); // { 21, 21, 21 ... }
    var ages2 = IntGenerator.Random(18, 60); // { random values between 18 and 60 }

Then, you combine your generators in interesting ways to create more complex objects:

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

Generators can be customised in all sorts of ways, allowing you to extend default object setup with more specialised modifications. Note that generators are immutable and all extension mechanisms result in a new generator object, leaving the originals unmodified:

    // Create a new generator will set a property based on values from another generator
    var niceColours = Generator.Values("Pink", "Red", "Green", "Lilac").Loop(4);
    var artisticUserGenerator = userGenerator.Set(c => c.FavouriteColour, niceColours);
    // Result:
    // { 
    //   {UserName: "user-0", DateOfBirth: 1999-06-06, FavouriteColour: "Pink" ...}, 
    //   {UserName: user-1, DateOfBirth: "2000-06-04", FavouriteColour: "Red" ... },
    //   ...
    // }

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

When managing a large or complex "hide your secrets, no setters" domain model, you can use BuildUp to create [Test Data Builders](http://c2.com/cgi/wiki?TestDataBuilder). Here's what you might come up with once you get the hang of it:

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


Can You Help?
-------------

This has been used successfully on a number of projects and I'm pleased with it so far. I often get the feeling that there are much smart peopler than me out there though. 

Feedback at this stage would be super-great, in particular:

* Serious suggestions for API improvements, no matter how radical!
* "Must-have" features that you'd need to use BuildUp on your own project

Add an issue or send me a message.

