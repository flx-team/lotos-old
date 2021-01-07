using System;

namespace Rovecode.Lotos.Common
{
    public interface IBinder<T>
    {
        void Attach(T value);

        void Detach(T value);
    }
}
