using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

[assembly:InternalsVisibleTo("UtilitiesTests")]
namespace Utilities
{
    public class Remover
    {
        public void RemoveTestFromDb(Guid guid)
        {
            RemoveTest(guid);
        }

        private void RemoveTest(Guid guid)
        {
            using (TestsContext context = new TestsContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    RemoveTest(context, guid);

                    transaction.Commit();
                }
            }
        }

        internal void RemoveTest(TestsContext context, Guid guid)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Test test = context.Tests.AsNoTracking()
                .Include(n => n.Questions)
                .ThenInclude(n => n.AnswerVariants)
                .Include(n => n.Image)
                .Include(n => n.Theory)
                .FirstOrDefault(n => n.Guid == guid.ToString());

            if (test == null)
                throw new InvalidOperationException($"Тест с guid = {guid} не существует в БД.");

            context.Remove(test);
            context.SaveChanges();
        }
    }
}
