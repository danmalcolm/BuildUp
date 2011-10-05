using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace BuildUp.Tests.Spikes
{
    [TestFixture]
    public class MultipleEnumerators
    {
        [Test]
        public void Multiple()
        {
            var s1 = new[] { 1, 2, 3, 4 };
            var s2 = new[] { "a", "b", "c", "d" };
            var s3 = new[] { "a", "b", "c", "d" };

            var sequences = new IEnumerable[] {s1, s2, s3};

            var enumerators = sequences.Select(x => x.GetEnumerator()).ToArray();
            var rows = new List<object[]>();
            while(enumerators.All(x => x.MoveNext()))
            {
                rows.Add(enumerators.Select(x => x.Current).ToArray());
            }
        }
    }
}