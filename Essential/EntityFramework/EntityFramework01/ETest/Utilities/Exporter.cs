using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

[assembly:InternalsVisibleTo("UtilitiesTests")]
namespace Utilities
{
    public class Exporter
    {
        public void ExportTestToXml(int id, string filePath)
        {
            Test test = ExmportTest(id);

            Serializer.Serialize(test, id, filePath);
        }

        private Test ExmportTest(int id)
        {
            using (TestsContext context = new TestsContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    return ExportTest(context, id);
                }
            }
        }

        internal Test ExportTest(TestsContext context, int id)
        {
            Test test = context.Tests.AsNoTracking()
                .Include(n => n.Questions)
                .ThenInclude(n => n.AnswerVariants)
                .Include(n => n.Image)
                .Include(n => n.Theory)
                .FirstOrDefault(n => n.Id == id);

            if (test == null)
                throw new InvalidOperationException($"Теста с id = {id} не существует в БД.");

            return test;
        }
    }
}
