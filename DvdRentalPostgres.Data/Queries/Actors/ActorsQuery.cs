using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DvdRentalPostgres.Data.Entities;
using DvdRentalPostgres.Data.Extensions;

namespace DvdRentalPostgres.Data.Queries.Actors
{
    public class ActorsQuery : DbOperation<IReadOnlyList<Actor>>
    {
        private readonly CriteriaJoinStrategy criteriaJoinStrategy;

        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ActorsQuery(IDbTransaction transaction, CriteriaJoinStrategy criteriaJoinStrategy = CriteriaJoinStrategy.And) 
            : base(transaction)
        {
            this.criteriaJoinStrategy = criteriaJoinStrategy;
        }

        public override async Task<IReadOnlyList<Actor>> Execute()
        {
            var builder = new WhereClauseBuilder("select * from actor", criteriaJoinStrategy);

            if (Id.HasValue)
                builder.AddClause("actor_id = @Id", new { Id = Id.Value });

            if (!string.IsNullOrEmpty(FirstName))
                builder.AddClause("first_name = @FirstName", new { FirstName });

            if(!string.IsNullOrEmpty(LastName))
                builder.AddClause("last_name = @LastName", new { LastName });

            var result  = builder.Build();
            var records = await Connection.QueryAsync<Actor>(result.BuiltQuery, result.BuiltParams, Transaction);
            return records.ToList().AsReadOnly();
        }
    }
}