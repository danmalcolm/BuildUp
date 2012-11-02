using System;

namespace BuildUp
{
	/// <summary>
	/// Generates a sequence of objects using values from one or more child generators
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IComplexGenerator<out T> : IGenerator<T>
	{
		/// <summary>
		/// Creates a new instance of this generator using the same function with child generators modified by the specified function
		/// </summary>
		/// <param name="change"></param>
		/// <returns></returns>
		IComplexGenerator<T> ChangeChildren(Func<ChildGeneratorCollection, ChildGeneratorCollection> change);
	}
}