using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PersonBook.Data.Core;
using PersonBook.Data.Dtos;
using PersonBook.Data.Mapping;
using PersonBook.Data.Model;

namespace PersonBook.Data.Repositories
{
    public class Repository<TEntity, TDto> : MapperReopsitory<TEntity, TDto>
        where TEntity : SerializableEntity, new()
        where TDto : Dto, new()
    {
        #region Fields

        private readonly IDataAccess _dataAccess;
        private Mapper<TEntity, TDto> _mapper;

        #endregion

        #region Constructor

        public Repository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #endregion

        #region IRepository members

        public override void Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreationDate = DateTime.Now;

            var dtos = _dataAccess.Read<TDto>();
            var dtosList = dtos?.OrderBy(a => a.CreationDate).ToList() ?? new List<TDto>();
            dtosList.Add(Mapper.Map(entity));

            _dataAccess.Write(dtosList);
        }

        public override TEntity Read(Guid id)
        {
            TEntity result = null;
            var dtos = _dataAccess.Read<TDto>();
            var outDto = dtos?.FirstOrDefault(a => a.Id == id);

            if (outDto != null)
            {
                result = Mapper.Map(outDto);
            }
            return result;
        }

        public override IEnumerable<TEntity> ReadAll()
        {
            var entities = _dataAccess.Read<TDto>();

            return Mapper.Map(entities);
        }

        public override void WriteAll(IEnumerable<TEntity> entities)
        {
            var dtos = Mapper.Map(entities);

            _dataAccess.Write(dtos);
        }

        public override void Update(TEntity entityToUpdate)
        {
            var dtos = _dataAccess.Read<TDto>();

            var dtosArray = dtos as TDto[] ?? dtos.ToArray();
            foreach (var dto in dtosArray)
            {
                if (dto.Id == entityToUpdate.Id)
                {
                    Mapper.Map(entityToUpdate, dto);
                }
            }

            _dataAccess.Write(dtosArray);
        }

        public override void Delete(Guid id)
        {
            var dtos = _dataAccess.Read<TDto>()?.ToList();

            var entity = dtos?.FirstOrDefault(a => a.Id == id);
            if (entity == null) return;

            dtos.Remove(entity);
            _dataAccess.Write(dtos);
        }

        #endregion

        #region MapperRepository members

        protected override Mapper<TEntity, TDto> Mapper => _mapper ?? (_mapper = GetMapper());

        #endregion


        #region Private methods

        private static Mapper<TEntity, TDto> GetMapper()
        {
            try
            {
                Mapper<TEntity, TDto> result = null;

                var mappers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType == typeof(Mapper<TEntity, TDto>));

                var mapperType = mappers.FirstOrDefault();
                if (mapperType != null)
                {
                    result = Activator.CreateInstance(mapperType) as Mapper<TEntity, TDto>;
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Cannot create specific mapper", ex);
                throw;
            }
        }

        #endregion

    }
}
