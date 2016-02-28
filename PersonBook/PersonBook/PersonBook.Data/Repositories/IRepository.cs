using System;
using System.Collections.Generic;
using PersonBook.Data.Dtos;
using PersonBook.Data.Model;

namespace PersonBook.Data.Repositories
{
    public interface IRepository<TEntity, TDto> 
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new ()
    {
        void Create(TEntity entity);
        TEntity Read(Guid id);
        IEnumerable<TEntity> ReadAll();
        void WriteAll(IEnumerable<TEntity> entities);
        void Update(TEntity entityToUpdate);
        void Delete(Guid id);
    }
}