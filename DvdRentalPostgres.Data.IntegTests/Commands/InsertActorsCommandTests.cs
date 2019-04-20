using System;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Commands.Actors;
using DvdRentalPostgres.Data.Entities;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Commands
{
    [Collection(DbTestsCollection.Name)]
    public class InsertActorsCommandTests
    {
        private readonly IDbConnectionFactory connectionFactory;

        public InsertActorsCommandTests(DbFixture fixture)
        {
            connectionFactory = fixture.DbConnectionFactory;
        }

        [Fact]
        public async Task Execute_InsertThreeActors_CountThreeReturned()
        {
            using (var connection = connectionFactory.NewConnection())
            {
                var txn = connection.BeginTransaction();
                var query = new InsertActorsCommand(txn)
                {
                    Actors = new[]
                    {
                        new Actor {ActorId = 201, FirstName = "Raghu", LastName = "Rana", LastUpdate = DateTime.Now},
                        new Actor {ActorId = 202, FirstName = "Som", LastName = "Rana", LastUpdate = DateTime.Now},
                        new Actor {ActorId = 203, FirstName = "Aria", LastName = "Rana", LastUpdate = DateTime.Now}
                    }
                };

                var insertCount = await query.Execute();
                Assert.Equal(3, insertCount);
            }
        }
    }
}