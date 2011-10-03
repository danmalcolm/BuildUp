namespace BuildUp
{
    /// <summary>
    /// Provides helper syntax
    /// </summary>
    public static class Use
    {
        public static T Source<T>(ISource<T> source)
        {
            return default(T);
        }

        public static T Existing<T>()
        {
            return default(T);
        }
    }
}