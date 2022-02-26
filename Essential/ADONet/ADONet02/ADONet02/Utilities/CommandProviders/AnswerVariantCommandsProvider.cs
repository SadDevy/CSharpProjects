using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Entities;

namespace Utilities.CommandProivders
{
    public static class AnswerVariantCommandsProvider
    {
        private const string exportAnswerVariantsQuery =
@"SELECT A.Id, A.QuestionId, A.Description, A.IsCorrected
FROM dbo.[Answer Variants] AS A 
WHERE A.QuestionId IN (
    SELECT Q.Id 
    FROM dbo.Questions AS Q 
    WHERE Q.TestId = @id);";

        private const string importAnswerVariantsQuery =
@"INSERT INTO dbo.[Answer Variants](QuestionId, Description, IsCorrected)
VALUES(@questionId, @description, @isCorrected);";

        private const string removeAnswerVariantsQuery =
@"DELETE FROM dbo.[Answer Variants]
WHERE QuestionId IN
    (SELECT Q.Id
     FROM dbo.Questions AS Q
     WHERE Q.TestId =
        (SELECT T.Id
         FROM dbo.Tests AS T
         WHERE T.Guid = @guid));";

        public static List<AnswerVariant> GetExportedAnswerVariants(SqlConnection connection, SqlTransaction transaction, int id)
        {
            List<AnswerVariant> answers = new List<AnswerVariant>();
            using (SqlCommand command = GetExportAnswerVariantsCommand(connection, id))
            {
                command.Transaction = transaction;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int answerIdOrdinal = reader.GetOrdinal("Id");
                    int questionIdOrdinal = reader.GetOrdinal("QuestionId");
                    int descriptionOrdinal = reader.GetOrdinal("Description");
                    int isCorrectedOrdinal = reader.GetOrdinal("IsCorrected");

                    while (reader.Read())
                    {
                        answers.Add(new AnswerVariant()
                        {
                            Id = reader.GetInt32(answerIdOrdinal),
                            QuestionId = reader.GetInt32(questionIdOrdinal),
                            Description = reader.GetString(descriptionOrdinal),
                            IsCorrected = reader.GetBoolean(isCorrectedOrdinal)
                        });
                    }
                }
            }

            return answers;
        }

        private static SqlCommand GetExportAnswerVariantsCommand(SqlConnection connection, int id)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            return CommandProvider.GetCommand(connection, exportAnswerVariantsQuery, id);
        }

        public static void ImportToDb(SqlConnection connection, SqlTransaction transaction, AnswerVariant answerVariant, int questionId)
        {
            using (SqlCommand command = GetImportCommand(connection, answerVariant, questionId))
            {
                command.Transaction = transaction;

                command.ExecuteNonQuery();
            }
        }

        private static SqlCommand GetImportCommand(SqlConnection connection, AnswerVariant answerVariant, int questionId)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (answerVariant == null)
                throw new ArgumentNullException(nameof(answerVariant));

            SqlParameter questionIdParameter = new SqlParameter("@questionId", SqlDbType.Int);
            questionIdParameter.Value = questionId;

            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
            descriptionParameter.Value = answerVariant.Description;

            SqlParameter isCorrectedParameter = new SqlParameter("@isCorrected", SqlDbType.Bit);
            isCorrectedParameter.Value = answerVariant.IsCorrected;

            return CommandProvider.GetCommand(connection, importAnswerVariantsQuery, questionIdParameter, descriptionParameter, isCorrectedParameter);
        }

        public static void RemoveFromDb(SqlConnection connection, SqlTransaction transaction, Guid guid)
        {
            using (SqlCommand removeAnswerVariantsCommand = GetRemoveCommand(connection, guid))
            {
                removeAnswerVariantsCommand.Transaction = transaction;

                removeAnswerVariantsCommand.ExecuteNonQuery();
            }
        }

        private static SqlCommand GetRemoveCommand(SqlConnection connection, Guid guid)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
            guidParameter.Value = guid.ToString();

            return CommandProvider.GetCommand(connection, removeAnswerVariantsQuery, guidParameter);
        }
    }
}
