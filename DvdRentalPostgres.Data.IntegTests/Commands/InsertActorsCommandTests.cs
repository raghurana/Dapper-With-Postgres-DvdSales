using System;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Commands.Actors;
using DvdRentalPostgres.Data.Entities;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Commands
{
    public class InsertActorsCommandTests : BaseDbTests
    {
        public InsertActorsCommandTests(DbFixture dbFixture) 
            : base(dbFixture)
        {}

        [Fact]
        public async Task Execute_InsertThreeActors_CountThreeReturned()
        {
            var query = new InsertActorsCommand(OpenTransaction)
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