using System;
using System.Threading.Tasks;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorageEntityProvider<T> where T : StorageEntity
    {
        public IStorage<T> Storage { get; }

        public T Value { get; }

        public void Remove();

        public bool Exists();

        public void Push();

        public void Pull();

        public void Shake();

        public Task PushAsync();

        public Task PullAsync();

        public Task ShakeAsync();
    }
}
