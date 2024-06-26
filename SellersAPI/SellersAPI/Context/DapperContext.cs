﻿namespace SellersAPI.Context
{
    using Microsoft.Data.SqlClient;
    using SellersAPI.Interfaces;
    using System.Data;

    public class DapperContext : IDbContext
    {
        private readonly string connectionString;

        public DapperContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(this.connectionString);
    }
}
