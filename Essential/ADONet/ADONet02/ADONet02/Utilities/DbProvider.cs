using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Utilities
{
    public static class DbProvider
    {
        private const string connectionStringName = "DefaultConnection";

        public static ConnectionStringSettings GetConnectionStringSettings() => ConfigurationManager.ConnectionStrings[connectionStringName];

        public static SqlConnection CreateDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString)); //!!!

            return new SqlConnection(connectionString);
        }
    }
}
