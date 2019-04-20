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

            var result  = builder.Build();
            var records =
                await Connection.QueryAsync<Film, Actor, Category, FilmsWithActorsCategories>(
                    result.BuiltQuery, 
                    Map,
                    result.BuiltParams, 
                    Transaction, 
                    splitOn: "actor_id, category_id");

            return records.ToList().AsReadOnly();
        }

        private FilmsWithActorsCategories Map(Film f, Actor a, Category c)
        {
            return new FilmsWithActorsCategories();
        }
    }
}