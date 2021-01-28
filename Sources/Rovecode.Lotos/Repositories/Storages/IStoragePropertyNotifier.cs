using System;

namespace Rovecode.Lotos.Repositories.Storages
{
    public interface IStoragePropertyNotifier<T>
    {
        public T Value { get; set; }

        public void Notify();
    }
}
