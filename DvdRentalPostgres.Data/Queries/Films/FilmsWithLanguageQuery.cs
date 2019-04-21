using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DvdRentalPostgres.Data.Entities;
using DvdRentalPostgres.Data.Extensions;

namespace DvdRentalPostgres.Data.Queries.Films
{
    public class FilmsWithLanguageQuery : DbOperation<IReadOnlyList<FilmWithLanguage>>
    {
        public string FilmTitle { get; set; }

        public FilmsWithLanguageQuery(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override async Task<IReadOnlyList<FilmWithLanguage>> Execute()
        {
            var query   = "select f.*, l.* from film f inner join language l on l.language_id = f.language_id";
            var builder = new WhereClauseBuilder(query);

            if(!string.IsNullOrEmpty(FilmTitle))
                builder.AddClause("f.title = @FilmTitle", new { FilmTitle });

            var result = builder.Build();
            var records =
                await Connection
                    .QueryAsync<Film, Language, FilmWithLanguage>(
                        result.BuiltQuery,
                        (f, l) => new FilmWithLanguage(f) { Lang = l }, 
                        result.BuiltParams,
                        Transaction, 
                        splitOn: "language_id");

            return records.Distinct().ToList().AsReadOnly();
        }
    }
}