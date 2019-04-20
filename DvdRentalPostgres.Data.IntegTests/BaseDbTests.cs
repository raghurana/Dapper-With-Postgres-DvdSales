using System;
using System.Data;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests
{
    [Collection(DbTestsCollection.Name)]
    public abstract class BaseDbTests : IDisposable
    {
        private IDbConnection connection;
        protected IDbTransaction OpenTransaction;

        protected BaseDbTests(DbFixture dbFixture)
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