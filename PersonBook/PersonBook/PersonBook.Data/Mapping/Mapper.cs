using System;
using System.Collections.Generic;
using PersonBook.Data.Dtos;
using PersonBook.Data.Model;

namespace PersonBook.Data.Mapping
{
    public abstract class Mapper<TModelEntity, TDto>
        where TModelEntity : SerializableEntity, new()
        where TDto : Dto, new()
    {
        public virtual TModelEntity Map(TDto source, TModelEntity destination)
        {
            destination.Id = source.Id;
            destination.CreationDate = source.CreationDate;
            return destination;
        }

        public virtual TDto Map(TModelEntity source, TDto destination)
        {
            destination.Id = source.Id;
            destination.CreationDate = source.CreationDate;
            return destination;
        }

        public TModelEntity Map(TDto source)
        {
            return Map(source, null);
        }

        public TDto Map(TModelEntity source)
        {
            return Map(source, null);
        }

        public IEnumerable<TModelEntity> Map(IEnumerable<TDto> sourceEntities,
            Action<TModelEntity> mappingActionCallback = null)
        {
            var destinationEntities = new List<TModelEntity>();
            if (sourceEntities != null)
            {
                foreach (var destinationElement in sourceEntities)
                {
                    var sourceEntity = Map(destinationElement, null);
                    if (sourceEntity == null) continue;

                    mappingActionCallback?.Invoke(sourceEntity);

                    destinationEntities.Add(sourceEntity);
                }
            }
            return destinationEntities;
        }

        public IEnumerable<TDto> Map(IEnumerable<TModelEntity> sourceEntities,
            Action<TDto> mappingActionCallback = null)
        {
            var destinationEntities = new List<TDto>();
            if (sourceEntities != null)
            {
                foreach (var sourceEntity in sourceEntities)
                {
                    var destinationEntity = Map(sourceEntity, null);
                    if (destinationEntity == null) continue;

                    mappingActionCallback?.Invoke(destinationEntity);

                    destinationEntities.Add(destinationEntity);
                }
            }
            return destinationEntities;
        }
    }
}
