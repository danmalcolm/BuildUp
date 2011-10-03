using System;

namespace BuildUp
{
	/// <summary>
	/// A type of source that uses other child sources when generating objects, such as values
	/// for constructor arguments. The Sources and Create members are exposed to allow 
	/// creation of new instances.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ICompositeSource<T> : ISource<T>
	{
		CtorArgSourceMap Sources { get; }

		Func<BuildContext, CtorArgSourceMap, T> CreateFunc { get; }
	}
}