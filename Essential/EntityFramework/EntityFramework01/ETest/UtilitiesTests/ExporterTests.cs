using System;
using NUnit.Framework;
using Utilities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace UtilitiesTests
{
    [TestFixture]
    public class ExporterTests
    {
        private static TestsContext context;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<TestsContext> builder = new DbContextOptionsBuilder<TestsContext>()
                .UseInMemoryDatabase("TestExporter")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            context = new TestsContext(builder.Options);
        }

        [Test]
        public void TestExportTest_HasTest_Success()
        {
            const int id = 6;
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
                context.Add(expected);
                context.SaveChanges();

                Exporter exporter = new Exporter();
                actual = exporter.ExportTest(context, id);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExportTest_HasNotTest_Exception()
        {
            const int id = -1;

            static void A()
            {
                using (context)
                {
                    Exporter exporter = new Exporter();
                    exporter.ExportTest(context, id);
                }
            }

            Assert.Throws<InvalidOperationException>(A, $"Теста с id = {id} не существует в БД.");
        }


    }
}
