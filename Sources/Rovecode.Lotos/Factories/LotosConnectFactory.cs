using System;
using MongoDB.Driver;
using Rovecode.Lotos.Contexts;

namespace Rovecode.Lotos.Factories
{
    public static class LotosConnectFactory
    {
        public static IStorageContext Connect(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);

            var db = client.GetDatabase(dbName);

            return new StorageContext(db!);
        }

        public static IStorageContext Connect(IMongoClient mongoClient, string dbName)
        {
            var db = mongoClient.GetDatabase(dbName);

            return new StorageContext(db!);
        }
    }
}
