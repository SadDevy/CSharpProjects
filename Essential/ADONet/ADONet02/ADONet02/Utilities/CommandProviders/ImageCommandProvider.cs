using System;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace Utilities.CommandProivders
{
    public static class ImageCommandProvider
    {
        private const string importTestImageQuery =
@"INSERT INTO dbo.Images(Img)
    OUTPUT INSERTED.Id
VALUES (@img);";

        public static int ImportToDb(SqlConnection connection, SqlTransaction transaction, Test test)
        {
            using (SqlCommand importImageCommand = GetImportImageCommand(connection, test.Image))
            {
                importImageCommand.Transaction = transaction;

                return (int)importImageCommand.ExecuteScalar();
            }
        }

        private static SqlCommand GetImportImageCommand(SqlConnection connection, byte[] image)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            SqlParameter imageParameter = new SqlParameter("@img", SqlDbType.VarBinary);
            imageParameter.Value = image;

            return CommandProvider.GetCommand(connection, importTestImageQuery, imageParameter);
        }
    }
}
