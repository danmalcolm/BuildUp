using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp.Generators
{
	/// <summary>
	/// Implementation that uses a function to generate each value
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public class SimpleGenerator<TObject, TContext> : IGenerator<TObject>
	{
		private readonly Func<TContext, int, TObject> create;
	    private readonly Func<TContext> createContext;

	    public SimpleGenerator(Func<TContext, int, TObject> create, Func<TContext> createContext)
        {
            this.create = create;
            this.createContext = createContext;
        }

	    public IEnumerable<TObject> Create()
		{
			int index = 0;
	        var context = createContext();
			while(true)
			{
				yield return this.create(context, index++);
			}
		}

		IEnumerable IGenerator.Create()
		{
			return Create();
		}
	}

    internal class EmptyContext
    {
        public static readonly EmptyContext Instance = new EmptyContext();
    }
}