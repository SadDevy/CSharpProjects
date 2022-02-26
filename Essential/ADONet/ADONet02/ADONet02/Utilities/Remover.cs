using System;
using System.Configuration;
using System.Data.SqlClient;
using Utilities.CommandProivders;

namespace Utilities
{
    public class Remover
    {
        private string connectionString;

        private Remover(ConnectionStringSettings settings) => connectionString = settings.ConnectionString;

        public static Remover CreateRemover()
        {
            ConnectionStringSettings settings = DbProvider.GetConnectionStringSettings();

            return new Remover(settings);
        }

        public void RemoveTest(Guid guid)
        {
            using (SqlConnection connection = DbProvider.CreateDbConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    AnswerVariantCommandsProvider.RemoveFromDb(connection, transaction, guid);
                    QuestionCommandsProvider.RemoveFromDb(connection, transaction, guid);
                    TestCommandsProvider.RemoveFromDb(connection, transaction, guid);

                    transaction.Commit();
                }
            }
        }
    }
}
