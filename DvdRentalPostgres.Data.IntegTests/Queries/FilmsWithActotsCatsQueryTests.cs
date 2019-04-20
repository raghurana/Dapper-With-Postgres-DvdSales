using System.Linq;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Queries.Films;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Queries
{
    public class FilmsWithActotsCatsQueryTests : BaseDbTests
    {
        public FilmsWithActotsCatsQueryTests(DbFixture dbFixture)
            : base(dbFixture)
        {}

        [Fact]
        public async Task Execute_WithExactFilmTitle_RecordsReturned()
        {
            var query = new FilmsWithActorsCategoriesQuery(OpenTransaction) { FilmTitle = "Airport Pollock" };
            var records = await query.Execute();

            Assert.Single(records);
            Assert.Equal(4, records.First().Actors.Count);
            Assert.Single(records.First().Categories);
        }

        [Fact]
        public async Task Execute_WithSearchFullText_RecordsReturned()
        {
            var query = new FilmsWithActorsCategoriesQuery(OpenTransaction) { SearchFullText = "Air:*" };
            var records = await query.Execute();

            Assert.Equal(5, records.Count);
        }
    }
}