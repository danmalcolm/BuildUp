using System;

namespace BuildUp
{
	public class CompositeSource
	{
		public static CompositeSource<T> Create<T>(Func<BuildContext, SourceMap, T> create, SourceMap sources)
		{
			return new CompositeSource<T>(create, sources);
		}
	}

	public class CompositeSource<T> : Source<T>, ICompositeSource<T>
	{
		private readonly Func<BuildContext, SourceMap, T> create;
		private readonly SourceMap sources;

		public CompositeSource(Func<BuildContext, SourceMap, T> create, SourceMap sources) 
			: base(c => create(c, sources)) // partially apply the source param
		{
			this.create = create;
			this.sources = sources;
		}

		SourceMap ICompositeSource<T>.Sources
		{
			get { return sources; }
		}

		Func<BuildContext, SourceMap, T> ICompositeSource<T>.Create
		{
			get { return create; }
		}
	}
}