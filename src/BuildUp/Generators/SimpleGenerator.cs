using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp.Generators
{
	/// <summary>
	/// Uses a function to generate objects
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

	    public IEnumerable<TObject> Build()
		{
			int index = 0;
	        var context = createContext();
			while(true)
			{
				yield return this.create(context, index++);
			}
		}

		IEnumerable IGenerator.Build()
		{
			return Build();
		}
	}

    internal class EmptyContext
    {
        public static readonly EmptyContext Instance = new EmptyContext();
    }
}