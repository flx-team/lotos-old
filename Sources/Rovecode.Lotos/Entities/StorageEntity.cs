using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Lotos.Exceptions;
using Lotos.Repositories;

namespace Lotos.Entities
{
    public abstract class StorageEntity<T> : IEntity, IStorageEntityRepository<T> where T : StorageEntity<T>
    {
        [BsonId]
        public Guid Id { get; internal set; }

        [BsonIgnore]
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public IStorage<T> Storage { get; internal set; } = null!;

        public Task<bool> Exists()
        {
            return Storage.Exists(id: Id);
        }

        public async Task Update()
        {
            if (!await Exists())
            {
                throw new LotosException("Current entity store in db");
            }

            await Storage.Update((T)this!);
        }

        public async Task Remove()
        {
            if (!await Exists())
            {
                throw new LotosException("Current entity store in db");
            }

            await Storage.Remove(Id);
        }

        public async Task<T> CopyActual()
        {
            var result = await Storage.Pick(Id);

            if (result is null)
            {
                throw new LotosException("Current entity store in db");
            }

            return result!;
        }
    }
}
