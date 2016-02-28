using Ninject;
using Ninject.Modules;

namespace PersonBook.Data.Infrastructure
{
    /// <summary>
    /// Allow to access kernel from any place of application - not very good idea but haven't got other
    /// http://stackoverflow.com/questions/25366291/how-to-handle-dependency-injection-in-a-wpf-mvvm-application
    /// </summary>
    public static class NinjectKernel
    {
        private static StandardKernel _kernel;
        private static readonly object SyncRoot = new object();

        public static T Get<T>()
        {
            lock (SyncRoot)
            {
                return _kernel.Get<T>();
            }
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }
    }
}
