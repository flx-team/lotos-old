﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotos.Contexts
{
    public sealed class SessionContext : IDisposable
    {
        public delegate Task SessionSandboxCallback(SessionContext context);

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

        public async Task Sandbox<T>(SessionSandboxCallback func) where T : Exception, new()
        {
            if (!IsStarted)
            {
                Start();
            }

            try
            {
                await func.Invoke(this);

                if (IsStarted)
                {
                    await CloseWithSuccess();
                }
            }
            catch (T)
            {
                if (IsStarted)
                {
                    await CloseWithError();
                }
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
