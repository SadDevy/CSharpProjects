using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Entities;

namespace Utilities
{
    public class Exporter
    {
        private string connectionString;

        private Exporter(ConnectionStringSettings settings)
            => connectionString = settings.ConnectionString;

        public static Exporter CreateExporter()
        {
            ConnectionStringSettings settings = DbProvider.GetConnectionStringSettings();
            return new Exporter(settings);
        }

        public void ExportTestToXml(int id, string filePath)
        {
            Test test = GetTest(id);

            Serializer.Serialize(test, id, filePath);
        }

        private Test GetTest(int id)
        {
            using (SqlConnection connection = DbProvider.CreateDbConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    Test test = CommandProivders.TestCommandsProvider.ExportFromDb(connection, transaction, id);
                    if (test == null)
                        throw new InvalidOperationException($"Теста с id={id} не существует в БД.");

                    test.Questions = GetQuestions(connection, transaction, id);
                    return test;
                }
            }
        }

        private List<Question> GetQuestions(SqlConnection connection, SqlTransaction transaction, int id)
        {
            List<AnswerVariant> answers = CommandProivders.AnswerVariantCommandsProvider.GetExportedAnswerVariants(connection, transaction, id);

            return CommandProivders.QuestionCommandsProvider.ExportFromDb(connection, transaction, answers, id);
        }
    }
}
