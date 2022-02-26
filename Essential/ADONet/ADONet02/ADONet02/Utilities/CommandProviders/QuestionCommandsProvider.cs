using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Entities;

namespace Utilities.CommandProivders
{
    public static class QuestionCommandsProvider
    {
        private const string exportQuestionsQuery =
@"SELECT Q.Id, Q.TestId, Q.Description
FROM dbo.Questions AS Q 
WHERE Q.TestId = @id;";

        private const string importQuestionsQuery =
@"INSERT INTO dbo.Questions(TestId, Description)
    OUTPUT INSERTED.Id
VALUES(@testId, @description);";

        private const string removeQuestionsQuery =
@"DELETE FROM dbo.Questions
WHERE TestId =
    (SELECT T.Id
     FROM dbo.Tests AS T
     WHERE T.Guid = @guid);";

        public static List<Question> ExportFromDb(SqlConnection connection, SqlTransaction transaction, List<AnswerVariant> answers, int id)
        {
            List<Question> questions = new List<Question>();
            using (SqlCommand command = GetExportCommand(connection, id))
            {
                command.Transaction = transaction;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int questionIdOrdinal = reader.GetOrdinal("Id");
                    int testIdOrdinal = reader.GetOrdinal("TestId");
                    int descriptionOrdinal = reader.GetOrdinal("Description");

                    while (reader.Read())
                    {
                        int questionId = reader.GetInt32(questionIdOrdinal);
                        questions.Add(new Question()
                        {
                            Id = questionId,
                            TestId = reader.GetInt32(testIdOrdinal),
                            Description = reader.GetString(descriptionOrdinal),
                            AnswerVariants = answers.Where(n => n.QuestionId == questionId).ToList()
                        });
                    }
                }
            }

            return questions;
        }

        private static SqlCommand GetExportCommand(SqlConnection connection, int id)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            return CommandProvider.GetCommand(connection, exportQuestionsQuery, id);
        }

        public static int ImportToDb(SqlConnection connection, SqlTransaction transaction, Question question, int testId)
        {
            using (SqlCommand command = GetImportQuestionCommand(connection, question, testId))
            {
                command.Transaction = transaction;

                return (int)command.ExecuteScalar();
            }
        }

        private static SqlCommand GetImportQuestionCommand(SqlConnection connection, Question question, int testId)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (question == null)
                throw new ArgumentNullException(nameof(question));

            SqlParameter testIdParameter = new SqlParameter("@testId", SqlDbType.Int);
            testIdParameter.Value = testId;

            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
            descriptionParameter.Value = question.Description;

            return CommandProvider.GetCommand(connection, importQuestionsQuery, testIdParameter, descriptionParameter);
        }

        public static void RemoveFromDb(SqlConnection connection, SqlTransaction transaction, Guid guid)
        {
            using (SqlCommand removeQuestionsCommand = GetRemoveCommand(connection, guid))
            {
                removeQuestionsCommand.Transaction = transaction;

                removeQuestionsCommand.ExecuteNonQuery();
            }
        }

        private static SqlCommand GetRemoveCommand(SqlConnection connection, Guid guid)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
            guidParameter.Value = guid.ToString();

            return CommandProvider.GetCommand(connection, removeQuestionsQuery, guidParameter);
        }
    }
}
