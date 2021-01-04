using System;
using Rovecode.Lotos.Enums;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorageDataRepository<T> : IDisposable where T : StorageData
    {
        public IStorage<T> Storage { get; }
        public T Data { get; }

        public void Exchange();
        public void Exchange(ExchangeMode mode);

        public void Burn();
        public bool Exist();
    }
}
