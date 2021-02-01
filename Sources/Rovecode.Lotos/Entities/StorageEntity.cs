using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Entities
{
    public abstract class StorageEntity<T> : IEntity, IStorageEntityRepository<T> where T : StorageEntity<T>
    {
        [BsonId]
        public Guid Id { get; internal set; }

        //[BsonIgnore]
        //[JsonIgnore]
        //[Newtonsoft.Json.JsonIgnore]
        //public IStorage<T> Storage { get;

        //[BsonIgnore]
        //[JsonIgnore]
        //[Newtonsoft.Json.JsonIgnore]
        //public IStorageEntityRepository<T> Repository { get; internal set; } = null!;

        [BsonIgnore]
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public IStorage<T> Storage { get; internal set; } = null!;

        public Task<bool> Exists()
        {
            return Storage.Exists(Id);
        }

        public Task Push()
        {
            return Storage.Push((T)this!);
        }

        public Task Remove()
        {
            return Storage.Remove(Id);
        }

        public Task<T> Pull()
        {
            return Storage.Pick(Id)!;
        }
    }
}
