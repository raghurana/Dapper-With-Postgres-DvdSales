using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Entities;

namespace DvdRentalPostgres.Data.Queries.Films
{
    public class FilmsWithActorsCategoriesQuery : DbOperation<IReadOnlyList<FilmsWithActorsCategories>>
    {
        public string FilmTitle { get; set; }

        public FilmsWithActorsCategoriesQuery(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override Task<IReadOnlyList<FilmsWithActorsCategories>> Execute()
        {

            return null;
        }
    }
}