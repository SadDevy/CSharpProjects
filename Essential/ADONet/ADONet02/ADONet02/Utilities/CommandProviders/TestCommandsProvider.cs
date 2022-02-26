using System;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace Utilities.CommandProivders
{
    public static class TestCommandsProvider
    {
        private const string exportTestQuery =
            @"SELECT T.Id, T.Name, TH.Description, TH.Url, I.Img, T.TestTime,
    T.QuestionsCount, T.CorrectedAnswersCount, T.TheoryIsShown 
FROM dbo.Tests AS T
    LEFT JOIN dbo.Theories AS TH ON TH.Id = T.TheoryId 
    LEFT JOIN dbo.Images AS I ON I.Id = T.ImageId
WHERE T.Id = @id;";

        private const string importTestQuery =
            @"INSERT INTO dbo.Tests(Guid, Name, TheoryId, ImageId, TestTime,
        QuestionsCount, CorrectedAnswersCount, TheoryIsShown)
    OUTPUT INSERTED.Id
VALUES(@guid, @name, @theoryId, @imageId, @testTime, @questionsCount,
       @correctedAnswersCount, @theoryIsShown);";

        private const string removeTestQuery =
            @"DELETE FROM dbo.Tests
WHERE Guid = @guid;";

        private const string testExistsQuery =
            @"SELECT CONVERT(bit, COUNT(T.Guid))
FROM dbo.Tests AS T
WHERE T.Guid = @guid;";

        public static Test ExportFromDb(SqlConnection connection, SqlTransaction transaction, int id)
        {
            Test test = null;
            using (SqlCommand command = GetExportCommand(connection, id))
            {
                command.Transaction = transaction;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int testIdOrdinal = reader.GetOrdinal("Id");
                    int testNameOrdinal = reader.GetOrdinal("Name");
                    int theoryOrdinal = reader.GetOrdinal("Description");
                    int theoryUrlOrdinal = reader.GetOrdinal("Url");
                    int imageOrdinal = reader.GetOrdinal("Img");
                    int testTimeOrdinal = reader.GetOrdinal("TestTime");
                    int questionsCountOrdinal = reader.GetOrdinal("QuestionsCount");
                    int rightCountOrdinal = reader.GetOrdinal("CorrectedAnswersCount");
                    int theoryIsShownOrdinal = reader.GetOrdinal("TheoryIsShown");

                    while (reader.Read())
                    {
                        test = new Test()
                        {
                            Id = reader.GetInt32(testIdOrdinal),
                            Name = reader.GetString(testNameOrdinal),
                            Theory = reader.IsDBNull(theoryOrdinal) ? null : reader.GetString(theoryOrdinal),
                            TheoryUrl = reader.IsDBNull(theoryUrlOrdinal) ? null : reader.GetString(theoryUrlOrdinal),

                            Image = reader.IsDBNull(imageOrdinal) ? null : GetImage(reader, imageOrdinal),

                            TestTime = reader.GetInt32(testTimeOrdinal),
                            QuestionsCount = reader.GetByte(questionsCountOrdinal),
                            RightCount = reader.GetByte(rightCountOrdinal),
                            TheoryIsShown = reader.GetBoolean(theoryIsShownOrdinal)
                        };
                    }
                }
            }

            return test;
        }

        private static SqlCommand GetExportCommand(SqlConnection connection, int id)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            return CommandProvider.GetCommand(connection, exportTestQuery, id);
        }

        private static byte[] GetImage(SqlDataReader reader, int index)
        {
            const int startIndex = 0;
            const int bufferSize = 100;

            byte[] outByte = new byte[bufferSize];
            if (reader.GetBytes(index, startIndex, outByte, 0, bufferSize) == 0)
                return null;

            return outByte;
        }

        public static int ImportToDb(SqlConnection connection, SqlTransaction transaction, Test test, Guid guid, int theoryId, int imageId)
        {
            
            using (SqlCommand importTestCommand = GetImportCommand(connection, test, guid, theoryId, imageId))
            {
                importTestCommand.Transaction = transaction;

                return (int)importTestCommand.ExecuteScalar();
            }
        }

        private static SqlCommand GetImportCommand(SqlConnection connection, Test test, Guid guid, int theoryId, int imageId)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (test == null)
                throw new ArgumentNullException(nameof(test));

            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
            guidParameter.Value = guid.ToString();

            SqlParameter nameParameter = new SqlParameter("@name", SqlDbType.NVarChar);
            nameParameter.Value = test.Name;

            SqlParameter theoryIdParameter = new SqlParameter("@theoryId", SqlDbType.Int);
            theoryIdParameter.Value = theoryId;

            SqlParameter imageIdParameter = new SqlParameter("@imageId", SqlDbType.Int);
            imageIdParameter.Value = imageId;

            SqlParameter testTimeParameter = new SqlParameter("@testTime", SqlDbType.Int);
            testTimeParameter.Value = test.TestTime;

            SqlParameter questionsCountParameter = new SqlParameter("@questionsCount", SqlDbType.SmallInt);
            questionsCountParameter.Value = test.QuestionsCount;

            SqlParameter correctedAnswersCountParameter = new SqlParameter("@correctedAnswersCount", SqlDbType.SmallInt);
            correctedAnswersCountParameter.Value = test.RightCount;

            SqlParameter theoryIsShownParameter = new SqlParameter("@theoryIsShown", SqlDbType.Bit);
            theoryIsShownParameter.Value = test.TheoryIsShown;

            return CommandProvider.GetCommand(connection, importTestQuery, guidParameter, nameParameter, theoryIdParameter, imageIdParameter, testTimeParameter,
                questionsCountParameter, correctedAnswersCountParameter, theoryIsShownParameter);
        }

        public static bool TestExists(SqlConnection connection, SqlTransaction transaction, Guid guid)
        {
            using (SqlCommand testExistsCommand = GetTestExistsCommand(connection, guid))
            {
                testExistsCommand.Transaction = transaction;

                return (bool)testExistsCommand.ExecuteScalar();
            }
        }

        private static SqlCommand GetTestExistsCommand(SqlConnection connection, Guid guid)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
            guidParameter.Value = guid.ToString();

            return CommandProvider.GetCommand(connection, testExistsQuery, guidParameter);
        }

        public static void RemoveFromDb(SqlConnection connection, SqlTransaction transaction, Guid guid)
        {
            using (SqlCommand removeTestCommand = GetRemoveCommand(connection, guid))
            {
                removeTestCommand.Transaction = transaction;

                removeTestCommand.ExecuteNonQuery();
            }
        }

        private static SqlCommand GetRemoveCommand(SqlConnection connection, Guid guid)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
            guidParameter.Value = guid.ToString();

            return CommandProvider.GetCommand(connection, removeTestQuery, guidParameter);
        }
    }
}
