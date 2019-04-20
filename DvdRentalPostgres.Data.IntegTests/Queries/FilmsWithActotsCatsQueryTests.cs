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
            var query = new FilmsWithActorsCategoriesQuery(OpenTransaction);
            query.FilmTitle = "Airport Pollock";

            var records = await query.Execute();
        }
    }
}