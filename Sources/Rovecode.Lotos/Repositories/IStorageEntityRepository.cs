using System;
using System.Threading.Tasks;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorageEntityRepository<T> where T : StorageEntity<T>
    {
        public IStorage<T> Storage { get; }

        public Task Remove();

        public Task<bool> Exists();

        public Task Push();

        public Task<T> Pull();
    }
}
