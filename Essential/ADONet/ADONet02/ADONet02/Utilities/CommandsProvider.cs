using System;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace Utilities
{
    public static class Tables1
    {
//        public static class Questions
//        {
//            private const string exportQuestionsQuery =
//@"SELECT Q.Id, Q.TestId, Q.Description
//FROM dbo.Questions AS Q 
//WHERE Q.TestId = @id;";

//            private const string importQuestionsQuery =
//@"INSERT INTO dbo.Questions(TestId, Description)
//    OUTPUT INSERTED.Id
//VALUES(@testId, @description);";

//            private const string removeQuestionsQuery =
//@"DELETE FROM dbo.Questions
//WHERE TestId =
//    (SELECT T.Id
//     FROM dbo.Tests AS T
//     WHERE T.Guid = @guid);";

//            public static SqlCommand GetExportQuestionsCommand(SqlConnection connection, int id) => GetCommand(connection, exportQuestionsQuery, id);

//            public static SqlCommand GetImportQuestionCommand(SqlConnection connection, Question question, int testId)
//            {
//                SqlParameter testIdParameter = new SqlParameter("@testId", SqlDbType.Int);
//                testIdParameter.Value = testId;

//                SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
//                descriptionParameter.Value = question.Description;

//                return GetCommand(connection, importQuestionsQuery, testIdParameter, descriptionParameter);
//            }

//            public static SqlCommand GetRemoveQuestionsCommand(SqlConnection connection, Guid guid)
//            {
//                SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//                guidParameter.Value = guid.ToString();

//                return GetCommand(connection, removeQuestionsQuery, guidParameter);
//            }
//        }

//        public static class AnswerVariants
//        {
//            private const string exportAnswerVariantsQuery =
//@"SELECT A.Id, A.QuestionId, A.Description, A.IsCorrected
//FROM dbo.[Answer Variants] AS A 
//WHERE A.QuestionId IN (
//    SELECT Q.Id 
//    FROM dbo.Questions AS Q 
//    WHERE Q.TestId = @id);";

//            private const string importAnswerVariantsQuery =
//@"INSERT INTO dbo.[Answer Variants](QuestionId, Description, IsCorrected)
//VALUES(@questionId, @description, @isCorrected);";

//            private const string removeAnswerVariantsQuery =
//@"DELETE FROM dbo.[Answer Variants]
//WHERE QuestionId IN
//    (SELECT Q.Id
//     FROM dbo.Questions AS Q
//     WHERE Q.TestId =
//        (SELECT T.Id
//         FROM dbo.Tests AS T
//         WHERE T.Guid = @guid));";

//            public static SqlCommand GetExportAnswerVariantsCommand(SqlConnection connection, int id) => GetCommand(connection, exportAnswerVariantsQuery, id);

//            public static SqlCommand GetRemoveAnswerVariantsCommand(SqlConnection connection, Guid guid)
//            {
//                SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//                guidParameter.Value = guid.ToString();

//                return GetCommand(connection, removeAnswerVariantsQuery, guidParameter);
//            }

//            public static SqlCommand GetImportCommand(SqlConnection connection, AnswerVariant answerVariant, int questionId)
//            {
//                SqlParameter questionIdParameter = new SqlParameter("@questionId", SqlDbType.Int);
//                questionIdParameter.Value = questionId;

//                SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
//                descriptionParameter.Value = answerVariant.Description;

//                SqlParameter isCorrectedParameter = new SqlParameter("@isCorrected", SqlDbType.Bit);
//                isCorrectedParameter.Value = answerVariant.IsCorrected;

//                return GetCommand(connection, importAnswerVariantsQuery, questionIdParameter, descriptionParameter, isCorrectedParameter);
//            }
//        }

//        public static class Tests
//        {    
//            private const string exportTestQuery =
//@"SELECT T.Id, T.Name, TH.Description, TH.Url, I.Img, T.TestTime,
//    T.QuestionsCount, T.CorrectedAnswersCount, T.TheoryIsShown 
//FROM dbo.Tests AS T
//    LEFT JOIN dbo.Theories AS TH ON TH.Id = T.TheoryId 
//    LEFT JOIN dbo.Images AS I ON I.Id = T.ImageId
//WHERE T.Id = @id;";

//            private const string importTestQuery = 
//@"INSERT INTO dbo.Tests(Guid, Name, TheoryId, ImageId, TestTime,
//        QuestionsCount, CorrectedAnswersCount, TheoryIsShown)
//    OUTPUT INSERTED.Id
//VALUES(@guid, @name, @theoryId, @imageId, @testTime, @questionsCount,
//       @correctedAnswersCount, @theoryIsShown);";

//            private const string removeTestQuery =
//@"DELETE FROM dbo.Tests
//WHERE Guid = @guid;";

//            private const string testExistsQuery =
//@"SELECT CONVERT(bit, COUNT(T.Guid))
//FROM dbo.Tests AS T
//WHERE T.Guid = @guid;";

//            public static SqlCommand GetRemoveTestCommand(SqlConnection connection, Guid guid)
//            {
//                SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//                guidParameter.Value = guid.ToString();

//                return GetCommand(connection, removeTestQuery, guidParameter);
//            }

//            public static SqlCommand GetExportTestCommand(SqlConnection connection, int id) => GetCommand(connection, exportTestQuery, id);

//            public static SqlCommand GetImportTestCommand(SqlConnection connection, Test test, Guid guid, int theoryId, int imageId)
//            {
//                SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//                guidParameter.Value = guid.ToString();

//                SqlParameter nameParameter = new SqlParameter("@name", SqlDbType.NVarChar);
//                nameParameter.Value = test.Name;

//                SqlParameter theoryIdParameter = new SqlParameter("@theoryId", SqlDbType.Int);
//                theoryIdParameter.Value = theoryId;

//                SqlParameter imageIdParameter = new SqlParameter("@imageId", SqlDbType.Int);
//                imageIdParameter.Value = imageId;

//                SqlParameter testTimeParameter = new SqlParameter("@testTime", SqlDbType.Int);
//                testTimeParameter.Value = test.TestTime;

//                SqlParameter questionsCountParameter = new SqlParameter("@questionsCount", SqlDbType.SmallInt);
//                questionsCountParameter.Value = test.QuestionsCount;

//                SqlParameter correctedAnswersCountParameter = new SqlParameter("@correctedAnswersCount", SqlDbType.SmallInt);
//                correctedAnswersCountParameter.Value = test.RightCount;

//                SqlParameter theoryIsShownParameter = new SqlParameter("@theoryIsShown", SqlDbType.Bit);
//                theoryIsShownParameter.Value = test.TheoryIsShown;

//                return GetCommand(connection, importTestQuery, guidParameter, nameParameter, theoryIdParameter, imageIdParameter, testTimeParameter,
//                    questionsCountParameter, correctedAnswersCountParameter, theoryIsShownParameter);
//            }

//            public static SqlCommand GetTestExistsCommand(SqlConnection connection, Guid guid)
//            {
//                SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//                guidParameter.Value = guid.ToString();

//                return GetCommand(connection, testExistsQuery, guidParameter);
//            }
//        }

//        public static class Theories
//        {
//            private const string importTheoryQuery =
//@"INSERT INTO dbo.Theories(Description, Url)
//    OUTPUT INSERTED.Id
//VALUES(@description, @url);";

//            public static SqlCommand GetImportTheoryCommand(SqlConnection connection, string description, string url)
//            {
//                SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NText);
//                descriptionParameter.Value = description;

//                SqlParameter urlParameter = new SqlParameter("@url", SqlDbType.NVarChar);
//                urlParameter.Value = url;

//                return GetCommand(connection, importTheoryQuery, descriptionParameter, urlParameter);
//            }
//        }

//        public static class Images
//        {
//            private const string importTestImageQuery =
//@"INSERT INTO dbo.Images(Img)
//    OUTPUT INSERTED.Id
//VALUES (@img);";

//            public static SqlCommand GetImportImageCommand(SqlConnection connection, byte[] image)
//            {
//                SqlParameter imageParameter = new SqlParameter("@img", SqlDbType.VarBinary);
//                imageParameter.Value = image;

//                return GetCommand(connection, importTestImageQuery, imageParameter);
//            }
//        }

//        private static SqlCommand GetCommand(SqlConnection connection, string query, int id)
//        {
//            SqlCommand command = new SqlCommand(query, connection);

//            SqlParameter parameter = command.Parameters.Add("@id", SqlDbType.Int);
//            parameter.Value = id;

//            return command;
//        }

//        private static SqlCommand GetCommand(SqlConnection connection, string query, params SqlParameter[] parameters)
//        {
//            SqlCommand command = new SqlCommand(query, connection);
//            command.Parameters.AddRange(parameters);

//            return command;
//        }
//    }

//    public static class CommandsProvider
//    {
//        private const string exportTestQuery =
//@"SELECT T.Id, T.Name, TH.Description, TH.Url, I.Img, T.TestTime,
//    T.QuestionsCount, T.CorrectedAnswersCount, T.TheoryIsShown 
//FROM dbo.Tests AS T
//    LEFT JOIN dbo.Theories AS TH ON TH.Id = T.TheoryId 
//    LEFT JOIN dbo.Images AS I ON I.Id = T.ImageId
//WHERE T.Id = @id;";

//        private const string exportQuestionsQuery =
//@"SELECT Q.Id, Q.TestId, Q.Description
//FROM dbo.Questions AS Q 
//WHERE Q.TestId = @id;";

//        private const string exportAnswerVariantsQuery =
//@"SELECT A.Id, A.QuestionId, A.Description, A.IsCorrected
//FROM dbo.[Answer Variants] AS A 
//WHERE A.QuestionId IN (
//    SELECT Q.Id 
//    FROM dbo.Questions AS Q 
//    WHERE Q.TestId = @id);";

//        private const string importTestQuery =
//@"INSERT INTO dbo.Tests(Guid, Name, TheoryId, ImageId, TestTime,
//        QuestionsCount, CorrectedAnswersCount, TheoryIsShown)
//    OUTPUT INSERTED.Id
//VALUES(@guid, @name, @theoryId, @imageId, @testTime, @questionsCount,
//       @correctedAnswersCount, @theoryIsShown);";

//        private const string importQuestionsQuery =
//@"INSERT INTO dbo.Questions(TestId, Description)
//    OUTPUT INSERTED.Id
//VALUES(@testId, @description);";

//        private const string importAnswerVariantsQuery =
//@"INSERT INTO dbo.[Answer Variants](QuestionId, Description, IsCorrected)
//VALUES(@questionId, @description, @isCorrected);";

//        private const string importTheoryQuery =
//@"INSERT INTO dbo.Theories(Description, Url)
//    OUTPUT INSERTED.Id
//VALUES(@description, @url);";

//        private const string testExistsQuery =
//@"SELECT CONVERT(bit, COUNT(T.Guid))
//FROM dbo.Tests AS T
//WHERE T.Guid = @guid;";

//        private const string removeAnswerVariantsQuery =
//@"DELETE FROM dbo.[Answer Variants]
//WHERE QuestionId IN
//    (SELECT Q.Id
//     FROM dbo.Questions AS Q
//     WHERE Q.TestId =
//        (SELECT T.Id
//         FROM dbo.Tests AS T
//         WHERE T.Guid = @guid));";

//        private const string removeQuestionsQuery =
//@"DELETE FROM dbo.Questions
//WHERE TestId =
//    (SELECT T.Id
//     FROM dbo.Tests AS T
//     WHERE T.Guid = @guid);";

//        private const string removeTestQuery =
//@"DELETE FROM dbo.Tests
//WHERE Guid = @guid;";

//        private const string importTestImageQuery =
//@"INSERT INTO dbo.Images(Img)
//    OUTPUT INSERTED.Id
//VALUES (@img);";

//        public static SqlCommand GetImportImageCommand(SqlConnection connection, byte[] image)
//        {
//            SqlParameter imageParameter = new SqlParameter("@img", SqlDbType.VarBinary);
//            imageParameter.Value = image;

//            return GetCommand(connection, importTestImageQuery, imageParameter);
//        }

//        public static SqlCommand GetRemoveAnswerVariantsCommand(SqlConnection connection, Guid guid)
//        {
//            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//            guidParameter.Value = guid.ToString();

//            return GetCommand(connection, removeAnswerVariantsQuery, guidParameter);
//        }

//        public static SqlCommand GetRemoveQuestionsCommand(SqlConnection connection, Guid guid)
//        {
//            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//            guidParameter.Value = guid.ToString();

//            return GetCommand(connection, removeQuestionsQuery, guidParameter);
//        }

//        public static SqlCommand GetRemoveTestCommand(SqlConnection connection, Guid guid)
//        {
//            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//            guidParameter.Value = guid.ToString();

//            return GetCommand(connection, removeTestQuery, guidParameter);
//        }

//        public static SqlCommand GetTestExistsCommand(SqlConnection connection, Guid guid)
//        {
//            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//            guidParameter.Value = guid.ToString();

//            return GetCommand(connection, testExistsQuery, guidParameter);
//        }

//        public static SqlCommand GetExportTestCommand(SqlConnection connection, int id) => GetCommand(connection, exportTestQuery, id);
//        public static SqlCommand GetExportQuestionsCommand(SqlConnection connection, int id) => GetCommand(connection, exportQuestionsQuery, id);
//        public static SqlCommand GetExportAnswerVariantsCommand(SqlConnection connection, int id) => GetCommand(connection, exportAnswerVariantsQuery, id);

//        public static SqlCommand GetImportTheoryCommand(SqlConnection connection, string description, string url)
//        {
//            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NText);
//            descriptionParameter.Value = description;

//            SqlParameter urlParameter = new SqlParameter("@url", SqlDbType.NVarChar);
//            urlParameter.Value = url;

//            return GetCommand(connection, importTheoryQuery, descriptionParameter, urlParameter);
//        }

//        public static SqlCommand GetImportTestCommand(SqlConnection connection, Test test, Guid guid, int theoryId, int imageId)
//        {
//            SqlParameter guidParameter = new SqlParameter("@guid", SqlDbType.NVarChar);
//            guidParameter.Value = guid.ToString();

//            SqlParameter nameParameter = new SqlParameter("@name", SqlDbType.NVarChar);
//            nameParameter.Value = test.Name;

//            SqlParameter theoryIdParameter = new SqlParameter("@theoryId", SqlDbType.Int);
//            theoryIdParameter.Value = theoryId;

//            SqlParameter imageIdParameter = new SqlParameter("@imageId", SqlDbType.Int);
//            imageIdParameter.Value = imageId;

//            SqlParameter testTimeParameter = new SqlParameter("@testTime", SqlDbType.Int);
//            testTimeParameter.Value = test.TestTime;

//            SqlParameter questionsCountParameter = new SqlParameter("@questionsCount", SqlDbType.SmallInt);
//            questionsCountParameter.Value = test.QuestionsCount;

//            SqlParameter correctedAnswersCountParameter = new SqlParameter("@correctedAnswersCount", SqlDbType.SmallInt);
//            correctedAnswersCountParameter.Value = test.RightCount;

//            SqlParameter theoryIsShownParameter = new SqlParameter("@theoryIsShown", SqlDbType.Bit);
//            theoryIsShownParameter.Value = test.TheoryIsShown;

//            return GetCommand(connection, importTestQuery, guidParameter, nameParameter, theoryIdParameter, imageIdParameter, testTimeParameter,
//                questionsCountParameter, correctedAnswersCountParameter, theoryIsShownParameter);
//        }

//        public static SqlCommand GetImportQuestionCommand(SqlConnection connection, Question question, int testId)
//        {
//            SqlParameter testIdParameter = new SqlParameter("@testId", SqlDbType.Int);
//            testIdParameter.Value = testId;

//            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
//            descriptionParameter.Value = question.Description;

//            return GetCommand(connection, importQuestionsQuery, testIdParameter, descriptionParameter);
//        }

//        public static SqlCommand GetImportCommand(SqlConnection connection, AnswerVariant answerVariant, int questionId)
//        {
//            SqlParameter questionIdParameter = new SqlParameter("@questionId", SqlDbType.Int);
//            questionIdParameter.Value = questionId;

//            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar);
//            descriptionParameter.Value = answerVariant.Description;

//            SqlParameter isCorrectedParameter = new SqlParameter("@isCorrected", SqlDbType.Bit);
//            isCorrectedParameter.Value = answerVariant.IsCorrected;

//            return GetCommand(connection, importAnswerVariantsQuery, questionIdParameter, descriptionParameter, isCorrectedParameter);
//        }

//        private static SqlCommand GetCommand(SqlConnection connection, string query, int id)
//        {
//            SqlCommand command = new SqlCommand(query, connection);

//            SqlParameter parameter = command.Parameters.Add("@id", SqlDbType.Int);
//            parameter.Value = id;

//            return command;
//        }

//        private static SqlCommand GetCommand(SqlConnection connection, string query, params SqlParameter[] parameters)
//        {
//            SqlCommand command = new SqlCommand(query, connection);
//            command.Parameters.AddRange(parameters);

//            return command;
//        }
    }
}
