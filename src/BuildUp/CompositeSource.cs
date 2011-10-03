using System;
using System.Linq.Expressions;

namespace BuildUp
{
    public class CompositeSource
    {
        public static CompositeSource<T> Create<T>(Func<BuildContext, CtorArgSourceMap, T> create,
                                                   CtorArgSourceMap sources)
        {
            return new CompositeSource<T>(create, sources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="create">The function used to create an object with the specified sources</param>
        /// <param name="source1">The source used to provide the first value used to create the object</param>
        /// <param name="source2">The source used to provide the second value used to create the object</param>
        /// <returns></returns>
        public static CompositeSource<T> Create<T, T1, T2>(Func<BuildContext, T1, T2, T> create, ISource<T1> source1,
                                                           ISource<T2> source2)
        {
            CtorArgSourceMap sources = new CtorArgSourceMap().Change(0, source1).Change(1, source2);
            return Create((c, s) =>
                              {
                                  var value1 = s.Create<T1>(0, c);
                                  var value2 = s.Create<T2>(1, c);
                                  return create(c, value1, value2);
                              }, sources);
        }

        /// <summary>
        /// Experimenting with new syntax for specifying sources for create function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="create"></param>
        /// <returns></returns>
        public static CompositeSource<T> Create<T>(Expression<Func<T>> create)
        {
            return null;
        }
    }

    public class CompositeSource<T> : Source<T>, ICompositeSource<T>
    {
        private readonly Func<BuildContext, CtorArgSourceMap, T> create;
        private readonly CtorArgSourceMap sources;

        public CompositeSource(Func<BuildContext, CtorArgSourceMap, T> create, CtorArgSourceMap sources)
            : base(c => create(c, sources)) // partially apply the source param
        {
            this.create = create;
            this.sources = sources;
        }

        #region ICompositeSource<T> Members

        CtorArgSourceMap ICompositeSource<T>.Sources
        {
            get { return sources; }
        }

        Func<BuildContext, CtorArgSourceMap, T> ICompositeSource<T>.Create
        {
            get { return create; }
        }

        #endregion
    }
}