using System;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Contexts
{
    public interface IStorageContext
    {
        public IStorage<T> Get<T>() where T : StorageData;
    }
}
