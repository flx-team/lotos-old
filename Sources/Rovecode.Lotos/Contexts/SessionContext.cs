using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rovecode.Lotos.Contexts
{
    public sealed class SessionContext : IDisposable
    {
        internal IClientSessionHandle Handle { get; }

        public bool IsStarted => Handle.IsInTransaction;

        public SessionContext(IMongoDatabase mongoDatabase)
        {
            Handle = mongoDatabase.Client.StartSession();
        }

        public void Start()
        {
            Handle.StartTransaction();
        }

        public Task CloseWithSuccess()
        {
            return Handle.CommitTransactionAsync();
        }

        public Task CloseWithError()
        {
            return Handle.AbortTransactionAsync();
        }

        public async Task Sandbox(Func<Task> func)
        {
            try
            {
                await func.Invoke();
                await CloseWithSuccess();
            }
            catch (Exception)
            {
                await CloseWithError();
                throw;
            }
        }

        public void Dispose()
        {
            if (IsStarted)
            {
                Handle.AbortTransaction();
            }

            Handle.Dispose();
        }
    }
}
