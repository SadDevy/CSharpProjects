using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Entities;
using Utilities.CommandProivders;

namespace Utilities
{
    public class Importer
    {
        private string connectionString;

        private Importer(ConnectionStringSettings settings)
            => connectionString = settings.ConnectionString;

        public static Importer CreateImporter()
        {
            ConnectionStringSettings settings = DbProvider.GetConnectionStringSettings();

            return new Importer(settings);
        }

        public void ImportTestFromXml(string filePath)
        {
            Test test = GetTestFromXml(filePath);

            ImportTest(test);
        }

        private void ImportTest(Test test)
        {
            using (SqlConnection connection = DbProvider.CreateDbConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    int testId = ImportTest(connection, transaction, test);
                    ImportQuestions(connection, transaction, test.Questions, testId);

                    transaction.Commit();
                }
            }
        }

        private Test GetTestFromXml(string filePath)
        {
            return Serializer.Deserialize(filePath);
        }

        private int ImportTest(SqlConnection connection, SqlTransaction transaction, Test test)
        {
            Guid guid = Guid.NewGuid();

            bool testExists = TestCommandsProvider.TestExists(connection, transaction, guid);
            if (testExists)
                throw new InvalidOperationException($"Тест с guid={guid} находится в БД.");

            int imageId = ImageCommandProvider.ImportToDb(connection, transaction, test);
            int theoryId = TheoryCommandsProvider.ImportToDb(connection, transaction, test.Theory, test.TheoryUrl);

            return TestCommandsProvider.ImportToDb(connection, transaction, test, guid, theoryId, imageId);
        }

        private void ImportQuestions(SqlConnection connection, SqlTransaction transaction, List<Question> questions, int testId)
        {
            foreach (Question question in questions)
            {
                int questionId = QuestionCommandsProvider.ImportToDb(connection, transaction, question, testId);
                ImportAnswerVariants(connection, transaction, question.AnswerVariants, questionId);
            }
        }

        private void ImportAnswerVariants(SqlConnection connection, SqlTransaction transaction, List<AnswerVariant> answerVariants, int questionId)
        {
            foreach (AnswerVariant answerVariant in answerVariants)
                AnswerVariantCommandsProvider.ImportToDb(connection, transaction, answerVariant, questionId);
        }
    }
}
