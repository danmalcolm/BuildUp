BUILDUP

A library to ease the pain of creating test objects during automated testing, or generating test / demo data for your application.


INTRODUCTION

If you're writing unit test.

You'll probably go through the following process:


Hmm, ok I need an object in my test. I'll create one.

Hmm, ok so now I've got lots of new Order() statements in each of my tests

Hmm, ok so I've just decided that I want to pass the customer to the constructor I've got loads of constructor calls to change. 

Obviously I shouldn't be newing up objects repeatedly in my tests

So I'll have a look around at the various patterns for doing this:

ObjectMother

CreateOrder, CreateOrderWithSomeProducts, CreateOrderWithAnOverseasProductsAndAnOutOfStockProduct

Builder seems more suitable, you have a class per type that you need to create. Then you add various methods for varying aspects of the object.

However, even though you have a lot more flexibility, you 

BuildUp picks up from the builder pattern but tries to add a little more composability to the mix:

Emphasise 

Make it easier to support common

Automatic Generation:

There are a few libraries out there that follow conventions and use reflection to set properties. ou

The emphasis of this library is on creating test objects in a behavioural domain model. This is where you typically change the state
of your objects using method calls:

var booking = new Booking(tour);
booking.AddAnonymousGuests();
var roomAssignments = new []
{
	new RoomAssignment("double", guest1, guest2);
	new RoomAssignment("twin", guest3, guest4);
};
booking.ProvisionallyReserveRooms();


Why do you do this:

Method calls allow state to be consistent
Meaningful operations make application code simpler while leaving state checking to the domain
Emphasises the _behaviour_ of your model. Your doing something, not changing properties or adding things to collections.


A model like this can't be set up using conventions as the behaviour is usually pretty specific and unique. So you need to think and write manual code to 
accommodate the test scenarios you're working with. The idea of BuildUp is that it provides some basic scaffolding so that you can declare these scenarios
and mix and match various bits.

It's so easy to create test data, that I don't rea

If you can create your data automatically, then the data you're  probably isn't.

You can quickly combine various sources of data as declaratively as possible
