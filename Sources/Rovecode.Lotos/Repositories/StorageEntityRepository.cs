using System;
using System.Threading.Tasks;
using Lotos.Entities;

namespace Lotos.Repositories
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

        public Task Update()
        {
            return Storage.Update(_entity);
        }

        public Task Remove()
        {
            return Storage.Remove(_entity.Id);
        }

        public Task<T> CopyActual()
        {
            return Storage.Pick(_entity.Id)!;
        }
    }
}
