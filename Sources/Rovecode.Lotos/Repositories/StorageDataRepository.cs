using System;
using Rovecode.Lotos.Enums;
using Rovecode.Lotos.Exceptions;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories
{
    public class StorageDataRepository<T> : IStorageDataRepository<T> where T : StorageData
    {
        public IStorage<T> Storage { get; }

        public T Data { get; private set; }

        public StorageDataRepository(IStorage<T> storage, T data)
        {
            Storage = storage;
            Data = data;
        }

        public void Burn()
        {
            Storage.Burn(e => e.Id == Data.Id);
        }

        public void Exchange()
        {
            Exchange(ExchangeMode.InOut);
        }

        public void Exchange(ExchangeMode mode)
        {
            switch (mode)
            {
                case ExchangeMode.InOut:
                    Push(Data);
                    Data = Recive(Data);
                    break;
                case ExchangeMode.In:
                    Push(Data);
                    break;
                case ExchangeMode.Out:
                    Data = Recive(Data);
                    break;
                default:
                    Exchange();
                    break;
            }
        }

        public void Push(T data)
        {
            if (!Exist())
            {
                // TODO: Add exception text
                throw new LotosException("");
            }

            var storage = (Storage as Storage<T>)!;

            var collection = storage.GetMongoCollection();

            var filter = storage.BuildWhereFilter(e => e.Id == data.Id);

            collection.ReplaceOne(filter, data);

        }

        public T Recive(T data)
        {
            return Storage.Search(e => e.Id == data.Id)!.Data;
        }

        public bool Exist()
        {
            return Storage.Exist(e => e.Id == Data.Id);
        }

        public void Dispose()
        {
            if (Exist())
            {
                Exchange();
            }
        }
    }
}
