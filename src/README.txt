BUILDUP

A library to ease the pain of creating test objects for use in unit tests, or for generating test / demo data.


INTRODUCTION

If you're writing unit tests against an object model, you'll probably go through the following process:

I need an Order object in my test, better create one...

OK, so now I've got lots of new Order() statements sprinkled around my tests. This is a bad thing, because I'll have loads of code to change if the constructor signature changes. I'm also repeating a lot of modifications to my orders, like adding lines, setting customers etc.

Obviously I shouldn't be newing up and setting up objects repeatedly in my tests. I'd better have a look around at the various patterns for doing this.

Hey, the ObjectMother pattern looks good (http://martinfowler.com/bliki/ObjectMother.html). 

OK, so the ObjectMother pattern is useful, but I've ended up with a load of methods like CreateOrderWithInStockProducts, 
CreateOrderWithAnOverseasShippingAddressAndAnOutOfStockProduct. It's hard to remember which tests are sharing these methods. It's also difficult to 
introduce smaller variations to the Orders being built. Is there a better approach?

Ah ha! The Test Data Builder pattern is what I'm after http://c2.com/cgi/wiki?TestDataBuilder. I can set up my OrderBuilder class to create Orders
with sensible defaults, then add methods to specify how Order objects are initialised.

There are numerous articles describing a similar evolution from manual object creation to Object Mother to the Test Data Builder pattern http://www.natpryce.com/articles/000714.html http://defragdev.com/blog/?p=147

BuildUp is designed to support the Test Data Builder pattern but tries to add a little more composability to the mix.

- Provides some common utility functionality, scaffolding and base classes to make new builders easy and quick to build with the minimum of duplication
- Emphasises a composable style
- Support for generating sequences of objects

A Note on Automatic Test Data Generation:

There are a few libraries out there that follow conventions and use reflection to set properties.

The emphasis of this library is on creating test objects in a behavioural domain model. This is where you typically change the state
of your objects using method calls:

var booking = new Booking(tour);
booking.AddAnonymousGuests(3);
var roomAssignments = new []
{
	new RoomAssignment("double", guest1, guest2);
	new RoomAssignment("twin", guest3, guest4);
};
booking.ProvisionallyReserveRooms(roomAssignments);

Why do you do this:

Method calls allow state to be consistent
Meaningful operations make application code simpler while leaving state checking to the domain
Emphasises the _behaviour_ of your model. You're doing something, not changing properties or adding things to collections.

A model like this can't be set up using conventions as the behaviour is usually pretty specific and unique. So you need to think and write manual code to 
accommodate the test scenarios you're working with. The idea of BuildUp is that it provides some basic scaffolding so that you can declare these scenarios
and mix and match various bits.

You can quickly combine various generators of data as declaratively as possible


WHY DOESN'T IGenerator<T> implement IEnumerable<T>

This was tempting but was not done for a few reasons. Instead, a method is called to trigger
creation of an IEnumerable by the generator, e.g. orderGenerator.Build();

from name in nameGenerator.Build()

arguments for (a generator is a sequence):
Viewing generator as a sequence of objects makes api a bit more concise, names.Take(4) etc is better than names.Build().Take(4)

against (a generator is a generator of a sequence, not the sequence itself!):
semantic collision, e.g. subtle differences in Select and SelectMany depending on whether extension methods are referenced
loss of composability - too easy for user to slip from working with immutable IGenerator<T> to IEnumerable<T>

compromise:

probably better for user to generate sequence explicitly

have a few extension methods so can access generated sequence, transitioning from IGenerator to IEnumerable:

Single
Take
Skip





