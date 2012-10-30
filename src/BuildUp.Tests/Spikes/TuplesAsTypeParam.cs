using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BuildUp.Tests.Spikes
{
	public class TuplesAsTypeParam
	{

		[Test]
		public void can_use_tuple_as_generic_type_param()
		{
			var i = new List<Tuple<int, string, decimal>>();
		}
	}
}