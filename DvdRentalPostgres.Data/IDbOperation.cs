using System.Data;
using System.Threading.Tasks;

namespace DvdRentalPostgres.Data
{
    public interface IDbOperation<TResult>
    {
        Task<TResult> Execute();
    }

    public abstract class DbOperation<TResult> : IDbOperation<TResult>
    {
        protected IDbTransaction Transaction { get; }

        protected IDbConnection Connection => Transaction.Connection;

        protected DbOperation(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public abstract Task<TResult> Execute();
    }
}