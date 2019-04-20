using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DvdRentalPostgres.Data.Entities;
using DvdRentalPostgres.Data.Extensions;

namespace DvdRentalPostgres.Data.Queries.Films
{
    public class FilmsWithActorsCategoriesQuery : DbOperation<IReadOnlyList<FilmsWithActorsCategories>>
    {
        public string FilmTitle { get; set; }

        public string SearchFullText { get; set; }

        public FilmsWithActorsCategoriesQuery(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override async Task<IReadOnlyList<FilmsWithActorsCategories>> Execute()
        {
            var builder = new WhereClauseBuilder(FilmsSql.SelectFilmsWitActorsAndCategories);

            if(!string.IsNullOrEmpty(FilmTitle))
                builder.AddClause("f.title = @FilmTitle", new { FilmTitle });

            if(!string.IsNullOrEmpty(SearchFullText))
                builder.AddClause("f.fulltext @@ to_tsquery(@SearchFullText)", new { SearchFullText });

            var dic1 = new Dictionary<int, FilmsWithActorsCategories>();
            var dic2 = new Dictionary<int, Actor>();
            var dic3 = new Dictionary<int, Category>();

            var result  = builder.Build();
            var records =
                await Connection.QueryAsync<Film, Actor, Category, FilmsWithActorsCategories>(
                    result.BuiltQuery, 
                    (f, a, c) => Map(f, a, c, dic1, dic2, dic3),
                    result.BuiltParams, 
                    Transaction, 
                    splitOn: "actor_id, category_id");

            return records.Distinct().ToList().AsReadOnly();
        }

        private FilmsWithActorsCategories Map(
            Film f, 
            Actor a, 
            Category c, 
            Dictionary<int, FilmsWithActorsCategories> dictResult, 
            Dictionary<int, Actor> dictActors, 
            Dictionary<int, Category> dictCategories)
        {

            if (!dictResult.TryGetValue(f.FilmId, out var fwac))
            {
                fwac = new FilmsWithActorsCategories(f);
                dictResult.Add(f.FilmId, fwac);
            }

            if (!dictActors.TryGetValue(a.ActorId, out var actor))
            {
                fwac.Actors.Add(a);
                dictActors.Add(a.ActorId, a);
            }
            else
            {
                fwac.Actors.Add(actor);
            }

            if (!dictCategories.TryGetValue(c.CategoryId, out var category))
            {
                fwac.Categories.Add(c);
                dictCategories.Add(c.CategoryId, c);
            }
            else
            {
                fwac.Categories.Add(category);
            }

            return fwac;
        }
    }
}