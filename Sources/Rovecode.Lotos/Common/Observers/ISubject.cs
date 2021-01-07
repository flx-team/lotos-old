using System;

namespace Rovecode.Lotos.Common.Observers
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }
}
