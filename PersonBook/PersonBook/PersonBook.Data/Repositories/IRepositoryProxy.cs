using PersonBook.Data.Dtos;
using PersonBook.Data.Model;

namespace PersonBook.Data.Repositories
{
    public delegate void CodeBlock<TEntity, TDto>(IRepository<TEntity, TDto> repository)
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new();

    public interface IRepositoryProxy<TEntity, TDto>
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new()
    {
        void Execute(CodeBlock<TEntity, TDto> codeBlock);
    }
}