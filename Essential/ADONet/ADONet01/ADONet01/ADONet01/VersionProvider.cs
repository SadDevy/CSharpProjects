using System;
using System.Data.Common;

namespace ADONet01
{
    public static class VersionProvider
    {
        public static string GetServerVersion(string providerName, string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            if (providerName == null)
                throw new ArgumentNullException(nameof(providerName));

            const string query = "SELECT @@version;";
            using (DbConnection connection = CreateConnection(providerName, connectionString))
            {
                connection.Open();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    return (string)command.ExecuteScalar();
                }
            }
        }

        private static DbConnection CreateConnection(string providerName, string connectionString)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                
                return connection;
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }
}
