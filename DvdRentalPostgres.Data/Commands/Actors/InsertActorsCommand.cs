using System.Data;
using System.Threading.Tasks;
using Dapper;
using DvdRentalPostgres.Data.Entities;

namespace DvdRentalPostgres.Data.Commands.Actors
{
    public class InsertActorsCommand : DbOperation<int>
    {
        public Actor[] Actors { get; set; }

        public InsertActorsCommand(IDbTransaction transaction) 
            : base(transaction)
        {}

        public override Task<int> Execute()
        {
            var query = "insert into actor values (@ActorId, @FirstName, @LastName, @LastUpdate)";
            return Connection.ExecuteAsync(query, Actors, Transaction);
        }
    }
}