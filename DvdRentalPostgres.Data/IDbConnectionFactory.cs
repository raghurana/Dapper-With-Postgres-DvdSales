using System.Data;
using Npgsql;

namespace DvdRentalPostgres.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection NewConnection();
    }

    public class NpgsqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public NpgsqlDbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection NewConnection()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}