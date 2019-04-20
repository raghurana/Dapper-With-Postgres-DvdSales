using System;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests
{
    public sealed class DbFixture : IDisposable
    {
        public IDbConnectionFactory DbConnectionFactory { get; private set; }

        public DbFixture()
        {
            var connectionString = ConnectionStringProvider.LocalPostgresConnectionString("dvdrental");
            DbConnectionFactory  = new NpgsqlDbConnectionFactory(connectionString);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public void Dispose()
        {
            DbConnectionFactory = null;
        }
    }

    [CollectionDefinition(Name)]
    public class DbTestsCollection : ICollectionFixture<DbFixture>
    {
        public const string Name = "DbTests";
    }
}