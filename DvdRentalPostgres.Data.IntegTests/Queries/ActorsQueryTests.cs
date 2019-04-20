using System.Threading.Tasks;
using DvdRentalPostgres.Data.Extensions;
using DvdRentalPostgres.Data.Queries.Actors;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Queries
{
    public class ActorsQueryTests : BaseDbTests
    {
        public ActorsQueryTests(DbFixture dbFixture) 
            : base(dbFixture)
        {}

        [Fact]
        public async Task Execute_NoQueryParams_AllActorsReturned()
        {
            var query = new ActorsQuery(OpenTransaction);
            var result = await query.Execute();
            Assert.Equal(200, result.Count);
        }

        [Fact]
        public async Task Execute_WithFirstNameAndLastName_OneActorReturned()
        {
            var query  = new ActorsQuery(OpenTransaction) { FirstName = "Adam", LastName = "Hopper" };
            var result = await query.Execute();

            Assert.Equal(1, result.Count);
            Assert.Equal("Adam", result[0].FirstName);
            Assert.Equal("Hopper", result[0].LastName);
        }

        [Fact]
        public async Task Execute_WithFirstNameOrLastName_FiveActorsReturned()
        {
            var query = new ActorsQuery(OpenTransaction, CriteriaJoinStrategy.Or)
            {
                FirstName = "Adam",
                LastName = "Willis"
            };

            var result = await query.Execute();
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task Execute_WithFirstNameAndLastName_ZeroActorsReturned()
        {
            var query = new ActorsQuery(OpenTransaction, CriteriaJoinStrategy.And)
            {
                FirstName = "Adam",
                LastName = "Willis"
            };

            var result = await query.Execute();
            Assert.Equal(0, result.Count);
        }
    }
}