using System;

namespace BuildUp
{
	/// <summary>
	/// Uses one or more child sources to provide values used to create objects
	/// </summary>
	/// <remarks>
	/// The ChildSources and CreateFunc members are exposed to simplify creation of new instances.
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public interface ICompositeSource<T> : ISource<T>
	{
		ChildSourceMap ChildSources { get; }

		Func<BuildContext, ChildSourceMap, T> CompCreateFunc { get; }
	}
}