using System;
using System.Data;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Commands
{
    [Collection(DbTestsCollection.Name)]
    public abstract class BaseDbCommandTests : IDisposable
    {
        private IDbConnection connection;
        protected IDbTransaction OpenTransaction;

        protected BaseDbCommandTests(DbFixture dbFixture)
        {
            connection = dbFixture.DbConnectionFactory.NewConnection();
            OpenTransaction = connection.BeginTransaction();
        }

        public void Dispose()
        {
            OpenTransaction.Rollback();
            connection.Dispose();

            OpenTransaction = null;
            connection = null;
        }
    }
}