using System;
using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTests
{
    [TestFixture]
    public class RemoverTests
    {
        private static TestsContext context;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TestsContext>()
                .UseInMemoryDatabase(nameof(RemoverTests))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            context = new TestsContext(dbContextOptions.Options);
        }

        [Test]
        public void TestRemove_HasTest_Success()
        {
            Guid guid = Guid.NewGuid();
            Test expected = new Test()
            {
                Guid = guid.ToString()
            };

            bool actual;
            using (context)
            {
                context.Add(expected);
                context.SaveChanges();
            }

            //context.Entry(expected).State = EntityState.Detached;
            using(TestsContext c = new TestsContext(new DbContextOptionsBuilder<TestsContext>()
                .UseInMemoryDatabase(nameof(RemoverTests))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options))
            {
                Remover remover = new Remover();
                remover.RemoveTest(c, guid);

                actual = c.Tests.Any(n => n.Guid == guid.ToString());
            }

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestRemove_HasNotTest_Exception()
        {
            Guid guid = Guid.NewGuid();
            void A()
            {
                using (context)
                {
                    Remover remover = new Remover();
                    remover.RemoveTest(context, guid);
                }
            }

            Assert.Throws<InvalidOperationException>(A, $"Тест с guid = {guid} не существует в БД.");
        }
    }
}
