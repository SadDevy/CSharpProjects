using System;
using System.Data;
using System.Data.SqlClient;

namespace Utilities.CommandProivders
{
    public static class CommandProvider
    {
        public static SqlCommand GetCommand(SqlConnection connection, string query, int id)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlParameter parameter = command.Parameters.Add("@id", SqlDbType.Int);
                parameter.Value = id;

                return command;
            }
        }

        public static SqlCommand GetCommand(SqlConnection connection, string query, params SqlParameter[] parameters)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);

                return command;
            }
        }
    }
}
