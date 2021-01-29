using System;
using MongoDB.Driver;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Containers
{
    public class Container : IContainer
    {
        public Container(IStorageContext storageContext)
        {
            Context = storageContext;

            ClientSessionHandle = MongoDatabase.Client.StartSession();
        }

        public IStorageContext Context { get; }

        public IClientSessionHandle ClientSessionHandle { get; }

        public IMongoDatabase MongoDatabase => (Context as StorageContext)!.MongoDatabase;

        public void Attach<T>(ref IStorage<T> storage) where T : StorageEntity
        {
            (storage as Storage<T>)!.Container = this;
        }

        public void Attach<T>(ref IStorageEntityProvider<T> provider) where T : StorageEntity
        {
            (provider.Storage as Storage<T>)!.Container = this;
        }

        public void Commit()
        {
            ClientSessionHandle.CommitTransaction();
        }

        public IStorage<T> GetStorage<T>() where T : StorageEntity
        {
            return new Storage<T>(this, MongoDatabase.GetCollection<T>(typeof(T).Name));
        }

        public void Revert()
        {
            ClientSessionHandle.AbortTransaction();
        }

        public void Run()
        {
            ClientSessionHandle.StartTransaction();
        }

        public void Sandbox(Action<IContainer> action)
        {
            Run();

            try
            {
                action.Invoke(this);
            }
            catch (Exception)
            {
                Revert();
                throw;
            }

            Commit();
        }
    }
}
