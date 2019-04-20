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

        public FilmsWithActorsCategoriesQuery(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override async Task<IReadOnlyList<FilmsWithActorsCategories>> Execute()
        {
            var query = @"select f.*, a.*, c.* from film f 
                          inner join film_actor fa on fa.film_id = f.film_id
                          inner join actor a on a.actor_id = fa.actor_id
                          inner join film_category fc on fc.film_id = f.film_id
                          inner join category c on c.category_id = fc.category_id";

            var builder = new WhereClauseBuilder(query);

            if(!string.IsNullOrEmpty(FilmTitle))
                builder.AddClause("f.title = @FilmTitle", new { FilmTitle });

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
                dictResult[f.FilmId] = fwac;
            }

            if (!dictActors.TryGetValue(a.ActorId, out var actor))
            {
                fwac.Actors.Add(a);
                dictActors[a.ActorId] = a;
            }
            else
            {
                fwac.Actors.Add(actor);
            }

            if (!dictCategories.TryGetValue(c.CategoryId, out var category))
            {
                fwac.Categories.Add(c);
                dictCategories[c.CategoryId] = c;
            }
            else
            {
                fwac.Categories.Add(category);
            }

            return fwac;
        }
    }
}