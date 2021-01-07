using System;

namespace Rovecode.Lotos.Common.Observers
{
    public interface IObserver
    {
        public void Update(ISubject subject);
    }
}
