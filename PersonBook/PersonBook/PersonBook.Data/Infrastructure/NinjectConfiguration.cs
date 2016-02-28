using Ninject.Modules;
using PersonBook.Data.Core;
using PersonBook.Data.Repositories;

namespace PersonBook.Data.Infrastructure
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepositoryProxy<,>))
                .To(typeof(RepositoryProxy<,>))
                .InThreadScope();

            Bind(typeof(IRepository<,>))
                .To(typeof(Repository<,>))
                .InThreadScope();

            Bind(typeof(IDataAccess))
                .To(typeof(DataAccess))
                .InThreadScope();
        }
    }
}
