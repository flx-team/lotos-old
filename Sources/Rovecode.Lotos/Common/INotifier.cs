using System;

namespace Rovecode.Lotos.Common
{
    public interface INotifier<T>
    {
        public void Notify(T data);
    }

    public interface INotifier
    {
        public void Notify();
    }
}
