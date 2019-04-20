using System;
using System.Data;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Commands
{
    [Collection(DbTestsCollection.Name)]
    public abstract class BaseDbCommandTests : IDisposable
    {
        protected IDbConnection Connection;
        protected IDbTransaction OpenTransaction;

        protected BaseDbCommandTests(DbFixture dbFixture)
        {
            Connection = dbFixture.DbConnectionFactory.NewConnection();
            OpenTransaction = Connection.BeginTransaction();
        }

        public void Dispose()
        {
            OpenTransaction.Rollback();
            Connection.Dispose();
            Connection = null;
        }
    }
}