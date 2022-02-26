using System;
using System.Data;
using System.Data.SqlClient;

namespace Utilities.CommandProivders
{
    public static class TheoryCommandsProvider
    {
        private const string importTheoryQuery =
@"INSERT INTO dbo.Theories(Description, Url)
    OUTPUT INSERTED.Id
VALUES(@description, @url);";

        public static int ImportToDb(SqlConnection connection, SqlTransaction transaction, string description, string url)
        {
            using (SqlCommand command = GetImportTheoryCommand(connection, description, url))
            {
                command.Transaction = transaction;

                return (int)command.ExecuteScalar();
            }
        }

        private static SqlCommand GetImportTheoryCommand(SqlConnection connection, string description, string url)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (description == null)
                throw new ArgumentNullException(nameof(description));

            if (url == null)
                throw new ArgumentNullException(nameof(url));

            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NText);
            descriptionParameter.Value = description;

            SqlParameter urlParameter = new SqlParameter("@url", SqlDbType.NVarChar);
            urlParameter.Value = url;

            return CommandProvider.GetCommand(connection, importTheoryQuery, descriptionParameter, urlParameter);
        }
    }
}
