using System.Threading.Tasks;
using DvdRentalPostgres.Data.Extensions;
using DvdRentalPostgres.Data.Queries.Actors;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Queries
{
    public class ActorsQueryTests
    {
        private readonly IDbConnectionFactory connectionFactory = new NpgsqlDbConnectionFactory(ConnectionStringProvider.LocalPostgresConnectionString("dvdrental"));

        public ActorsQueryTests()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        [Fact]
        public async Task Execute_NoQueryParams_AllActorsReturned()
        {
            using (var conn = connectionFactory.NewConnection())
            {
                var query = new ActorsQuery(conn.BeginTransaction());
                var result = await query.Execute();
                Assert.Equal(200, result.Count);
            }
        }

        [Fact]
        public async Task Execute_WithFirstNameAndLastName_OneActorReturned()
        {
            using (var conn = connectionFactory.NewConnection())
            {
                var query = new ActorsQuery(conn.BeginTransaction()) {FirstName = "Adam", LastName = "Hopper"};
                var result = await query.Execute();
                Assert.Equal(1, result.Count);
                Assert.Equal("Adam", result[0].FirstName);
                Assert.Equal("Hopper", result[0].LastName);
            }
        }

        [Fact]
        public async Task Execute_WithFirstNameOrLastName_FiveActorsReturned()
        {
            using (var conn = connectionFactory.NewConnection())
            {
                var query = new ActorsQuery(conn.BeginTransaction(), CriteriaJoinStrategy.Or)
                {
                    FirstName = "Adam",
                    LastName = "Willis"
                };

                var result = await query.Execute();
                Assert.Equal(5, result.Count);
            }
        }
    }
}