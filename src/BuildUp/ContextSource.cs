using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BuildUp
{
	/// <summary>
	/// Provides a sequence of CreateContext instances that include values from a collection of child sources
	/// </summary>
	public class ContextSource : IEnumerable<CreateContext>
	{
		#region Creation methods

		/// <summary>
		/// Provides CreateContext sequence containing only Index value
		/// </summary>
		/// <returns></returns>
		public static ContextSource Create()
		{
			return new ContextSource(new ChildSourceMap());
		}

		/// <summary>
		/// Provides CreateContext sequence with each item containing values provided by a collection of child sources.
		/// </summary>
		/// <param name="childSources"></param>
		/// <returns></returns>
		public static ContextSource Create(ChildSourceMap childSources)
		{
			return new ContextSource(childSources);
		}

		#endregion

		public ChildSourceMap ChildSources { get; private set; }

		internal ContextSource(ChildSourceMap childSources)
		{
			this.ChildSources = childSources;
		}

		public IEnumerator<CreateContext> GetEnumerator()
		{
			return ChildSources.Tuplize().Select((tuple, index) => new CreateContext(index, tuple)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}