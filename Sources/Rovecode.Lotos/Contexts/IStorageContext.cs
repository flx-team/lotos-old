using System;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories;
using Rovecode.Lotos.Repositories.Storages;

namespace Rovecode.Lotos.Contexts
{
    public interface IStorageContext
    {
        /// <summary>
        /// Gets IStorage related to generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IStorage<T> Get<T>() where T : StorageData;
    }
}
