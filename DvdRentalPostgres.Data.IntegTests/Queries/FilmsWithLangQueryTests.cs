using System.Linq;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Queries.Films;
using Xunit;

namespace DvdRentalPostgres.Data.IntegTests.Queries
{
    public class FilmsWithLangQueryTests : BaseDbTests
    {
        public FilmsWithLangQueryTests(DbFixture dbFixture) 
            : base(dbFixture)
        {}

        [Fact]
        public async Task Execute_WithExactFilmTitle_OneToOneRelationLanguagePopulated()
        {
            var q = new FilmsWithLanguageQuery(OpenTransaction) {FilmTitle = "Ace Goldfinger"};
            var records = await q.Execute();

            Assert.Equal(1, records.Count);
            Assert.StartsWith("English", records.First().Lang.Name);
        }
    }
}