using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

[assembly:InternalsVisibleTo("UtilitiesTests")]
namespace Utilities
{
    public class Importer
    {
        public void ImportTestFromXml(string filePath)
        {
            Test test = Serializer.Deserialize(filePath);

            ImportTest(test);
        }

        private void ImportTest(Test test)
        {
            using (TestsContext context = new TestsContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    ImportTest(context, test);

                    transaction.Commit();
                }
            }
        }

        private void ImportTest(TestsContext context, Test test)
        {
            Guid guid = Guid.NewGuid();
            ImportTest(context, test, guid);
        }

        internal void ImportTest(TestsContext context, Test test, Guid guid)
        {
            if (HasTest(context, guid))
                throw new InvalidOperationException($"Тест с guid = {guid} существует в БД.");
            
            test.Guid = guid.ToString();
            context.Add(test);
            context.SaveChanges();
        }

        private bool HasTest(TestsContext context, Guid guid)
        {
            return context.Tests.AsNoTracking().Any(n => n.Guid == guid.ToString());
        }
    }
}