using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Exceptions;

namespace Rovecode.Lotos.Factories
{
    public static class LotosConnectFactory
    {
        public static IStorageContext Connect(string connectionString, string dbName)
        {
            return Connect(new MongoClient(connectionString), dbName);
        }

        public static IStorageContext Connect(IMongoClient mongoClient, string dbName)
        {
            var db = mongoClient.GetDatabase(dbName);

            if (!IsMongoLive(db, 1000))
            {
                throw new FailConnectLotosException("Fail connect to the mongo db.");
            }

            return new StorageContext(db!);
        }

        public static bool IsMongoLive(IMongoDatabase mongoDatabase, int millisecondsTimeout)
        {
            return mongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(millisecondsTimeout);
        }
    }
}
