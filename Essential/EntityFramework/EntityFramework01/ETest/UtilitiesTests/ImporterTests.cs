using System;
using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTests
{
    [TestFixture]
    public class ImporterTests
    {
        private static TestsContext context;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TestsContext>()
                .UseInMemoryDatabase(nameof(ImporterTests))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            context = new TestsContext(dbContextOptions.Options);
        }

        [Test]
        public void TestImportTest_HadNotTest_Success()
        {
            const int id = 1;
            const string testName = "Test";
            Guid guid = Guid.NewGuid();

            Test expected = new Test()
            {
                Id = id,
                Guid = guid.ToString(),
                Name = testName
            };

            Test actual;
            using (context)
            {
                Importer importer = new Importer();
                importer.ImportTest(context, expected, guid);

                actual = context.Tests.First();
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestImportTest_HasTest_Exception()
        {
            Guid guid = Guid.NewGuid();
            void A()
            {
                Test test = new Test()
                {
                    Guid = guid.ToString(),
                };

                using (context)
                {
                    Importer importer = new Importer();
                    importer.ImportTest(context, test, guid);

                    importer.ImportTest(context, test, guid);
                }
            }

            Assert.Throws<InvalidOperationException>(A, $"Тест с guid = {guid} существует в БД.");
        }
    }
}
