using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuildUp.Generators;
using BuildUp.Tests.Common;
using NUnit.Framework;

namespace BuildUp.Tests.Generators.GuidGeneratorSpecs
{
    public class SequenceSpecs
    {

        [Test]
        public void generators_using_same_seed_should_produce_identical_sequences()
        {
            var generator1 = GuidGenerator.Random(10);
            var generator2 = GuidGenerator.Random(10);
            var sequence1 = generator1.Take(10).ToArray();
            var sequence2 = generator2.Take(10).ToArray();
            sequence1.ShouldMatchSequence(sequence2);
        }

        [Test]
        public void should_create_identical_sequences_when_repeated()
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
            const int count = 10000;
            var guids = new ConcurrentDictionary<Guid,int>();
            var watch = new Stopwatch();
            watch.Start();
            int seed = minSeed;
            while(seed <= maxSeed)
            {
                var generator1 = GuidGenerator.Random(seed);
                foreach (var guid in generator1.Take(count))
                {
                    guids.AddOrUpdate(guid, 1, (g, c) => c + 1);
                }
                seed++;
            }

//            Parallel.For(minSeed, maxSeed + 1, seed =>
//            {
//                var generator1 = GuidGenerator.Random(seed);
//                foreach (var guid in generator1.Take(count))
//                {
//                    guids.AddOrUpdate(guid, 1, (g, c) => c + 1);
//                }
//            });
            watch.Stop();
            Console.WriteLine("Ran test in {0}ms", watch.ElapsedMilliseconds);
            guids.Keys.LongCount().ShouldEqual((maxSeed + 1 - minSeed) * count);
        } 
    }
}