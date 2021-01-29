using System;
using System.Threading.Tasks;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Repositories
{
    public class StorageEntityProvider<T> : IStorageEntityProvider<T> where T : StorageEntity
    {
        public IStorage<T> Storage { get; }

        public T Value { get; private set; }

        public StorageEntityProvider(IStorage<T> storage, T value)
        {
            Storage = storage;
            Value = value;
        }

        public bool Exists()
        {
            return Storage.Exists(Value.Id);
        }

        public void Pull()
        {
            Value = Storage.Pick(Value.Id)!.Value;
        }

        public async Task PullAsync()
        {
            Value = (await Storage.PickAsync(Value.Id))!.Value;
        }

        public void Push()
        {
            // TODO: Refactor
            var storage = (Storage<T>)Storage;
            var handler = storage.GetContainer().ClientSessionHandle;

            var filter = storage.BuildWhereFilter(e => e.Id == Value.Id);

            var collection = storage.GetMongoCollection();

            collection.ReplaceOne(handler, filter, Value);
        }

        public async Task PushAsync()
        {
            // TODO: Refactor
            var storage = (Storage<T>)Storage;
            var handler = storage.GetContainer().ClientSessionHandle;

            var filter = storage.BuildWhereFilter(e => e.Id == Value.Id);

            var collection = storage.GetMongoCollection();

            await collection.ReplaceOneAsync(handler, filter, Value);
        }

        public void Remove()
        {
            Storage.Remove(Value.Id);
        }

        public void Shake()
        {
            Push();
            Pull();
        }

        public async Task ShakeAsync()
        {
            await PushAsync();
            await PullAsync();
        }
    }
}
