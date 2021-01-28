using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Rovecode.Lotos.Enums;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories.Storages
{
    public class StorageDataRepository<T> : IStorageDataRepository<T> where T : StorageData
    {
        public IStorage<T> Storage { get; }

        public T Data { get; }

        public StorageDataRepository(IStorage<T> storage, T data)
        {
            Storage = storage;
            Data = data;

            var props = Data.GetType().GetProperties().Where(e => e.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IStoragePropertyNotifier<>)));

            foreach (var item in props)
            {
                object? value = item.GetValue(Data, null);
                item.PropertyType.GetMethod("Notify")?.Invoke(value, null);
            }
        }

        public void Burn()
        {
            Storage.Burn(e => e.Id == Data.Id);
        }

        public bool Exists()
        {
            return Storage.Exists(e => e.Id == Data.Id);
        }
    }
}
