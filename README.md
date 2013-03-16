BuildUp
=======

Introduction
------------

A library to ease the pain of creating objects for automated testing or demo data.

Examples:



Purpose
-------

If you're writing unit tests against an object model, this may be familiar:

"I need an Order object in my test, better create one..."

"Hmm, now I've got lots of new Order() statements sprinkled throughout my tests (and my colleages are getting annoyed with me for humming Blue Monday). I'm also repeating a lot of order modification logic, like adding lines, setting customers etc. I'll have loads of code to change if the constructor signature changes and there's lots of repetition. Obviously I shouldn't be newing up and duplicating set-up code in my tests."

"Hey, the [ObjectMother](http://martinfowler.com/bliki/ObjectMother.html) pattern looks good. Yep, definitely useful, but I've ended up with a load of methods like CreateOrderWithInStockProducts, 
CreateOrderWithAnOverseasShippingAddressAndAnOutOfStockProduct. It's hard to remember which tests are sharing these methods. It's also difficult to introduce smaller variations to the Orders being built. Is there a better approach?"

"Ah ha! The [Test Data Builder](http://c2.com/cgi/wiki?TestDataBuilder) pattern is what I'm after. I can create an OrderBuilder class that creates Orders with some sensible default values, then add methods to customise how my Orders are initialised when they are built. "

There are numerous articles describing a similar evolution from manual object creation to Object Mother to the Test Data Builder pattern http://www.natpryce.com/articles/000714.html http://defragdev.com/blog/?p=147

So, is our intrepid developer happy? What happens next?

"I'm a lot happier with my object builders. They seem a little bit clunky to build though, lots of repetition. What if I need to build a list of objects, it's hard to write builder classes that support this."

BuildUp is designed to support the Test Data Builder pattern. It also provides an object creation API that advances beyond the idea of having a single monolithic builder class and adds a little more composability to the mix.

- As we saw in the examples above, everything is a sequence
- Emphasises a declarative, composable style, allowing objects to be generated and combined in interesting ways
- Provides some common utility functionality, scaffolding and base classes to make new builders easy and quick to build with the minimum of duplication


Using Generators
----------------

Generators are used to create sequences of objects. BuildUp contains some primitive object generators that support the kinds of sequences that you might need to work with.


Using Builders
--------------

Generators can get you quite a long way. However the compositional nature of the API introduces a problem. Imagine an Order class with the following constructor: 

public class Order 
{
    public Order(Customer customer, DateTime date) 
    {
        this.Customer = customer;
        ...
    }

    public Customer Customer { get; private set; }
}

The Customer to which an Order belongs is set only via the constructor. In the above example, we demonstrated various methods to specify modifications to the generated objects. 

An order generator might be created as follows:

var customers = ...
var dates = ...
var orders = from c in customers
    from d in dates
    select new Order(c, d);

There doesn't seem to be a suitable way of configuring a generator to control the values passed to the constructor. We need some kind of state that we can access to define and modify constructor parameters.

This is where builders come in. Builders are a custom class that you create, inheriting from BuilderBase<T> e.g. 

public class OrderBuilder : BuilderBase<T>
{
 //TODO example
}

Notice 4 things:
- We have fields (properties can be used also) used to store generators used to generate the objects
- You aren't limited to using generators to generate child values, e.g. single value is used for the date.
- The GetGenerator method returns a generator that generates the objects using the builder's generators and values
- We have meaningful methods to modify these fields. If you have a behavioural domain model where state is changed via meaningful methods (instead of setters or directly adding / removing items from collections), then there may be methods on the builder that match the target object's behaviour. You don't have to do things this way. With a more data-centric model, you could allow the values to be modified via properties.

This puts us back in control - we can vary the values passed to the target constructor by changing the state of the builder.

Mutable Builders
----------------
Mutable builders - When you vary a value, the 

Immutable builders - A builder's method calls actually return a new builder instance. This prevents errors and also promotes reuse.



API Design Decisions
--------------------

Why does IGenerator<T> have a Build() method that you need to call to create the sequence? Couldn't IGenerator<T> just implement IEnumerable<T>?
-------------------------------------------------------------------------------------------------

Think of generators as things that contains the logic used to create a sequence. They are not the sequence themselves.

Benefits:

1. It makes its purpose clearer. For example, a unit test class might have the following field:

private IGenerator<Customer> customers;

It's a semantic issue, but I think it reinforces the fact that a fresh sequence of objects will be generated every time we call customers.Build(). This communicates that we can safely reuse the generator to generate the objects used in each individual test. Using IEnumerable would lead to doubts, e.g. "Can I mutate these objects without having side-effects on other tests?", "Will I get the same or a different set of objects every time I enumerate the sequence?".

2. Generators provide a framework for defining how objects are generated in a declarative way. Some operations available on IEnumerable wouldn't make sense in this context, e.g. ElementAt, Reverse etc. Other things are simply done differently, e.g. the SelectMany operator has been implemented to support concise code when combining multiple generators, but is closer to the Zip extension method than SelectMany.

3. BuildUp defines extension methods for IGenerator<T> that are specific to building up test data. Developers may add extension methods specific to their object model. Defining these on IEnumerable<T> would "pollute" code that works with IEnumerable<T> throughout your application. This is similar to the distinction between Rhino Mocks and Moq.


A Note on Automatic Test Data Generation
----------------------------------------

There are a few libraries out there that automate generation of test data, by making intelligent guesses about properties.

BuildUp focuses on the creation of test objects within a behavioural domain model and requires you to do some thinking about providing meaningful data. This is where you typically change the state
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

A model like this can't be set up using conventions as the behaviour is usually pretty specific and unique. So you need to think and write manual code to accommodate the test scenarios you're working with. The idea of BuildUp is that it provides some basic scaffolding for you to specify this data in a nice way.


