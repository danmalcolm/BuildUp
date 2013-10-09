using System.Linq;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using FluentAssertions;
using Xunit;

namespace BuildUp.Tests.Generators.ModifySequenceExtensionsSpecs
{
    public class ModifySequenceSpecs
    {
        [Fact]
        public void should_apply_sequence_modification()
        {
            var generator = Generator.Create(index => index).ModifySequence(x => x.Skip(2));
            generator.Take(3).Should().Equal(2, 3, 4);
        }

        [Fact]
        public void should_not_modify_original_generator()
        {
            var generator1 = Generator.Create(index => index);
            var generator2 = generator1.ModifySequence(x => x.Skip(2));
            generator1.Take(3).Should().Equal(0, 1, 2);
        }
    }

    public class FreezeSpecs
    {
        [Fact]
        public void should_repeat_first_value()
        {
            var generator = Generator.Create(index => index).Freeze();
            generator.Take(5).Should().Equal(0, 0, 0, 0, 0);
        }
    }

    public class LoopSpecs
    {
        [Fact]
        public void should_loop_values()
        {
            var generator1 = Generator.Create(index => index);
            var generator2 = generator1.Loop(3);

            generator1.Take(9).Should().Equal(0, 1, 2, 3, 4, 5, 6, 7, 8);
            generator2.Take(9).Should().Equal(0, 1, 2, 0, 1, 2, 0, 1, 2);
        }

        [Fact]
        public void looping_complex_objects_should_repeat_same_object_instances()
        {
            var generator = from name in StringGenerator.Numbered("Mr Man {1}")
                            from age in Generator.Constant(38)
                            select new Person(name, age);
            generator = generator.Loop(2);
            generator.Take(4).Distinct().Should().HaveCount(2);
        }
    }
    public class RepeatEachSpecs
    {
        [Fact]
        public void should_repeat_each_value_the_specified_number_of_times()
        {
            var generator = Generator.Values(1, 2, 3).RepeatEach(3);
            generator.Take(9).Should().Equal(1, 1, 1, 2, 2, 2, 3, 3, 3);
        }

        [Fact]
        public void repeat_each_with_generator_of_complex_objects_should_repeat_same_instances()
        {
            var generator = from name in StringGenerator.Numbered("Mr Man {1}")
                            from age in Generator.Constant(38)
                            select new Person(name, age);
            generator = generator.RepeatEach(3);
            generator.Take(6).Distinct().Should().HaveCount(2);
        }
    }

    class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string FavouriteColour { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
