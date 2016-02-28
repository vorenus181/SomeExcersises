using System;
using PersonBook.Data.Dtos;
using PersonBook.Data.Infrastructure;
using PersonBook.Data.Model;

namespace PersonBook.Data.Repositories
{
    public class RepositoryProxy<TEntity, TDto> : IRepositoryProxy<TEntity, TDto> 
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new()
    {
        public void Execute(CodeBlock<TEntity, TDto> codeBlock)
        {
            try
            {
                var repository = NinjectKernel.Get<IRepository<TEntity, TDto>>();
                codeBlock(repository);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
