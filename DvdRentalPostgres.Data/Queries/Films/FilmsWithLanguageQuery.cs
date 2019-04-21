using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DvdRentalPostgres.Data.Entities;

namespace DvdRentalPostgres.Data.Queries.Films
{
    public class FilmsWithLanguageQuery : DbOperation<IReadOnlyList<FilmWithLanguage>>
    {
        public FilmsWithLanguageQuery(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override Task<IReadOnlyList<FilmWithLanguage>> Execute()
        {

        }
    }
}