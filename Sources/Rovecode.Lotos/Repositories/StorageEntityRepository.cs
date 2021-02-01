using System;
using System.Threading.Tasks;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Repositories
{
    public class StorageEntityRepository<T> : IStorageEntityRepository<T> where T : StorageEntity<T>
    {
        public IStorage<T> Storage { get; }

        private readonly T _entity;

        public StorageEntityRepository(IStorage<T> storage, T entity)
        {
            Storage = storage;
            _entity = entity;
        }

        public Task<bool> Exists()
        {
            return Storage.Exists(_entity.Id);
        }

        public Task Push()
        {
            return Storage.Push(_entity);
        }

        public Task Remove()
        {
            return Storage.Remove(_entity.Id);
        }
    }
}
