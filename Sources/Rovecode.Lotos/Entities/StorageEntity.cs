using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Entities
{
    public abstract class StorageEntity<T> : IEntity where T : StorageEntity<T>
    {
        [BsonId]
        public Guid Id { get; internal set; }

        [BsonIgnore]
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public IStorage<T> Storage => Repository.Storage;

        [BsonIgnore]
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public IStorageEntityRepository<T> Repository { get; internal set; } = null!;
    }
}
