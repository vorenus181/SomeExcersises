using System;
using System.Collections.Generic;
using PersonBook.Data.Dtos;
using PersonBook.Data.Mapping;
using PersonBook.Data.Model;

namespace PersonBook.Data.Repositories
{
    public abstract class MapperReopsitory<TEntity, TDto> : IRepository<TEntity, TDto>
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new()
    {
        public abstract void Create(TEntity entity);

        public abstract TEntity Read(Guid id);

        public abstract IEnumerable<TEntity> ReadAll();

        public abstract void WriteAll(IEnumerable<TEntity> entities);
        
        public abstract void Update(TEntity entityToUpdate);

        public abstract void Delete(Guid id);

        protected abstract Mapper<TEntity, TDto> Mapper { get; }
    }
}
