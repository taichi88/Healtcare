using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HealthcareApi.Infrastructure.Data.Dapper.DapperDbContext
{
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not set.");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
