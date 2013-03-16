using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuildUp.ValueGenerators;
using NUnit.Framework;
using BuildUp.Tests.Common;

namespace BuildUp.Tests.ValueGenerators
{
    public class GuidGeneratorsSpecs
    {
        
        [Test]
        public void different_generators_using_same_seed_should_produce_identical_sequences()
        {
            var generator1 = GuidGenerator.Incrementing(10);
            var generator2 = GuidGenerator.Incrementing(10);
            var sequence1 = generator1.Take(10).ToArray();
            var sequence2 = generator2.Take(10).ToArray();
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
                var generator1 = GuidGenerator.Incrementing(seed);
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