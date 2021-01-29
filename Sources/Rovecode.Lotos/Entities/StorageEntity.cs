using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rovecode.Lotos.Entities
{
    public abstract class StorageEntity
    {
        [BsonId]
        public ObjectId Id { get; }

        public StorageEntity()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
