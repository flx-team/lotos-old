using System;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Containers
{
    public interface IContainer
    {
        public IStorageContext Context { get; }

        public IStorage<T> GetStorage<T>() where T : StorageEntity;

        public void Run();

        public void Revert();

        public void Commit();

        public void Sandbox(Action<IContainer> action);

        public void Attach<T>(ref IStorage<T> storage) where T : StorageEntity;

        public void Attach<T>(ref IStorageEntityProvider<T> provider) where T : StorageEntity;
    }
}
