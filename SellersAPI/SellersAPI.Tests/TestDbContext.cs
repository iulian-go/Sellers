namespace SellersAPI.Tests
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using SellersAPI.Interfaces;
    using System.Data;

    internal class TestDbContext : IDbContext
    {
        private readonly string connectionString;

        public TestDbContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("SqlTestConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(this.connectionString);
    }
}
