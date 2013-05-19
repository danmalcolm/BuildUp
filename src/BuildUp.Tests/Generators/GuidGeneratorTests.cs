using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators
{
    public class GuidGeneratorTests
    {
        
        [Test]
        public void incrementing_generators_using_same_seed_should_produce_identical_sequences()
        {
            var generator1 = GuidGenerator.Random(10);
            var generator2 = GuidGenerator.Random(10);
            var sequence1 = generator1.Take(10).ToArray();
            var sequence2 = generator2.Take(10).ToArray();
            sequence1.ShouldMatchSequence(sequence2);
        }

        [Test]
        public void incrementing_generator_should_repeat_identical_sequences()
        {
            var generator1 = GuidGenerator.Random(10);
            var sequence1 = generator1.Take(10).ToArray();
            var sequence2 = generator1.Take(10).ToArray();
            sequence1.ShouldMatchSequence(sequence2);
        }

        [Test,Explicit]
        public void should_not_generate_duplicates()
        {
            const int minSeed = 1;
            const int maxSeed = 1000;
            const int count = 1000;
            var guids = new ConcurrentDictionary<Guid,int>();
            var watch = new Stopwatch();
            watch.Start();
            Parallel.For(minSeed, maxSeed, seed =>
            {
                var generator1 = GuidGenerator.Random(seed);
                foreach (var guid in generator1.Take(count))
                {
                    guids.AddOrUpdate(guid, 1, (g, c) => c + 1);
                }
            });
            watch.Stop();
            Console.WriteLine("Ran test in {0}ms", watch.ElapsedMilliseconds);
            guids.Keys.LongCount().ShouldEqual((maxSeed - minSeed) * (long)count);
        } 
    }
}