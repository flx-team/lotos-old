using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Factories;

namespace Rovecode.Lotos.DependencyInjection.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddLotos(this IServiceCollection services, string connectionString, string dbName)
        {
            var storageContext = LotosConnectFactory.Connect(connectionString, dbName);

            AddLotosServices(ref services, storageContext);
        }

        public static void AddLotos(this IServiceCollection services, IMongoClient mongoClient, string dbName)
        {
            var storageContext = LotosConnectFactory.Connect(mongoClient, dbName);

            AddLotosServices(ref services, storageContext);
        }

        private static void AddLotosServices(ref IServiceCollection services, IStorageContext storageContext)
        {
            services.AddSingleton(storageContext);

            services.AddTransient<IContainer, Container>();
        }
    }
}
