using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Rovecode.Lotos.Factories;

namespace Rovecode.Lotos.DependencyInjection.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddLotos(this IServiceCollection service, string connectionString, string dbName)
        {
            var storageContext = LotosConnectFactory.Connect(connectionString, dbName);

            service.AddSingleton(storageContext);
        }

        public static void AddLotos(this IServiceCollection service, IMongoClient mongoClient, string dbName)
        {
            var storageContext = LotosConnectFactory.Connect(mongoClient, dbName);

            service.AddSingleton(storageContext);
        }
    }
}
