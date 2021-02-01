using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Exceptions;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Extensions
{
    public static class DiExtension
    {
        public static void AddLotos(this IServiceCollection services, Assembly assembly)
        {
            services.AddMongoClient();
            services.AddMongoDatabase();
            services.AddRepositories(assembly);
        }

        public static void AddLotos(this IServiceCollection services)
        {
            services.AddLotos(Assembly.GetCallingAssembly());
        }

        private static void AddMongoClient(this IServiceCollection services)
        {
            if (!services.IsMongoRegistered())
            {
                services.AddSingleton(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();

                    var configurationSection = configuration.GetSection("Lotos");

                    if (configurationSection is null)
                        throw new LotosException("Lotos сonfiguration not found");

                    var connection = configurationSection["Connection"];

                    if (connection is null)
                        throw new LotosException("Сonnection string not found");

                    return new MongoClient(connection);
                });
            }
        }

        private static void AddMongoDatabase(this IServiceCollection services)
        {
            if (services.IsMongoRegistered())
            {
                services.AddSingleton(provider =>
                {
                    var mongoClient = provider.GetRequiredService<MongoClient>();

                    var db = mongoClient.GetDatabase("Lotos");

                    if (!db.IsMongoLive(10000))
                        throw new LotosException("Unable to connect to database");

                    return db!;
                });

                services.AddScoped(typeof(StorageContext<>));
            }
        }

        private static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var repositoryTypes = assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IStorage<>)))
                .ToArray();

            foreach (var type in repositoryTypes)
            {
                services.AddScoped(type);
            }

            services.AddScoped(typeof(IStorage<>), typeof(Storage<>));
        }

        private static bool IsMongoRegistered(this IServiceCollection services)
        {
            var isMongo = services.Any(e => e.ServiceType == typeof(MongoClient));

            return isMongo;
        }

        private static bool IsMongoLive(this IMongoDatabase mongoDatabase, int millisecondsTimeout)
        {
            return mongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
                .Wait(millisecondsTimeout);
        }
    }
}
