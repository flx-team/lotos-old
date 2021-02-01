using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Rovecode.Lotos.Exceptions;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Utils
{
    public static class StorageUtils
    {
        public static FilterDefinition<T> BuildIdFilter<T>(T entity) where T : IEntity
        {
            Guid id = (Guid)entity.Id!;

            return BuildIdFilter<T>(id);
        }

        public static FilterDefinition<T> BuildIdFilter<T>(Guid id) where T : IEntity
        {
            return Builders<T>.Filter.Eq(e => e.Id, id);
        }

        public static FilterDefinition<T> BuildIdsFilter<T>(IEnumerable<Guid> ids) where T : IEntity
        {
            switch (ids.Count())
            {
                case 0:
                    throw new LotosException("IEnumerable (ids) is empty. It must contain at least one id!");
                case 1:
                    return BuildIdFilter<T>(ids.First());
                default:
                    return Builders<T>.Filter.In(e => (Guid)e.Id!, ids);
            }
        }

        public static FilterDefinition<T> BuildIdsFilter<T>(IEnumerable<T> entities) where T : IEntity
        {
            var ids = entities.Select(e => (Guid)e.Id!);

            return BuildIdsFilter<T>(ids);
        }

    }
}
