BuildUp
=======

A library to ease the pain of creating test objects for use in unit tests, or for generating test / demo data.

Introduction
------------

If you're writing unit tests against an object model, you'll probably go through the following process:

I need an Order object in my test, better create one...

Hmm, I've written quite a few tests and now I've got lots of new Order() statements sprinkled throughout. I'm also repeating a lot of order modification logic, like adding lines, setting customers etc. This is a bad thing, because I'll have loads of code to change if the constructor signature changes.  Obviously I shouldn't be newing up and setting up objects repeatedly in my tests. I'd better have a look around at the various patterns for doing this. Hey, the [ObjectMother](http://martinfowler.com/bliki/ObjectMother.html) pattern looks good. 

OK, so the ObjectMother pattern is useful, but I've ended up with a load of methods like CreateOrderWithInStockProducts, 
CreateOrderWithAnOverseasShippingAddressAndAnOutOfStockProduct. It's hard to remember which tests are sharing these methods. It's also difficult to 
introduce smaller variations to the Orders being built. Is there a better approach?

Ah ha! The [Test Data Builder](http://c2.com/cgi/wiki?TestDataBuilder) pattern is what I'm after. I can create an OrderBuilder class that creates Orders
using some sensible default steps, then add methods to customise how my Orders are initialised when they are built.

There are numerous articles describing a similar evolution from manual object creation to Object Mother to the Test Data Builder pattern http://www.natpryce.com/articles/000714.html http://defragdev.com/blog/?p=147

BuildUp is designed to support the Test Data Builder pattern but tries to add a little more composability to the mix.

- Provides some common utility functionality, scaffolding and base classes to make new builders easy and quick to build with the minimum of duplication
- Emphasises a composable style, allowing data generators to be combined in interesting ways
- Supports generation of sequences of objects

Examples
--------


Why go to all this trouble of creating IGenerator<T>? Why not just work with IEnumerable<T>?
--------------------------------------------------------------------------------------------

Think of a generator as a container that holds the logic used to create a sequence. IGenerator<T> implementations contain logic to generate sequences, they are not the sequence themselves. 

This explicit separation makes it simple to recreate new sequences for each test. Imagine if you had ten unit tests within a test fixture, each of which starts with a sequence of orders in a known state, exerts some behaviour and tests for expected changes in the state. You'll need to create a fresh set of orders for each test. If you were creating the sequence manually you'd probably have a CreateCustomers method using procedural code. Generators provide a framework for defining how objects are generated in a declarative way.

There are a large number of extension methods available for IGenerator<T> that are specific to building up test data. Defining these on IEnumerable<T> would "pollute" code that works with IEnumerable<T> throughout your application.

It also gives us more options for composing generators. Generators themselves are immutable and a range of methods exist to combine them together in interesting ways.


A Note on Automatic Test Data Generation
----------------------------------------

There are a few libraries out there that follow conventions and use reflection to set properties.

BuildUp focuses on the creation of test objects within a behavioural domain model. This is where you typically change the state
of your objects using method calls:

```
var booking = new Booking(tour);
booking.AddAnonymousGuests(4);
var roomAssignments = new []
{
	new RoomAssignment("double", booking.Guests[0], booking.Guests[1]);
	new RoomAssignment("twin", booking.Guests[2], booking.Guests[3]);
};
booking.AssignRooms(roomAssignments);
```

Why do you do this:

Emphasises the _behaviour_ of your model. You're doing something, not changing properties or adding things to collections.
Meaningful operations make application code simpler while leaving state checking to the domain

A model like this can't be set up using conventions as the behaviour is usually pretty specific and unique. So you need to think and write manual code to 
accommodate the test scenarios you're working with. The idea of BuildUp is that it provides some basic scaffolding so that you can declare these scenarios
and mix and match various bits.


