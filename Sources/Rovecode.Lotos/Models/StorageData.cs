using System;
using MongoDB.Bson;

namespace Rovecode.Lotos.Models
{
    public record StorageData
    {
        public object Id { get; private set; } = ObjectId.GenerateNewId();

    }
}
