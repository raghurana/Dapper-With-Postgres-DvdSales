namespace DvdRentalPostgres.Data.IntegTests
{
    public static class ConnectionStringProvider
    {
        public static string LocalPostgresConnectionString(string dbName)
        {
            return $"Server=localhost;Port=5432;Database={dbName};Integrated Security=true;";
        }
    }
}