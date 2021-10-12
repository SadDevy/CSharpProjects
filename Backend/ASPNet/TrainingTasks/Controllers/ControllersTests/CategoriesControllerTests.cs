using System.Collections.Generic;
using System.Linq;
using Introduction.Controllers;
using Introduction.Data.Repositories;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ControllersTests
{
    [TestFixture]
    public class CategoriesControllerTests
    {
        [Test]
        public void Index_ReturnAViewResult_WithCategoriesList()
        {
            Mock<IRepository<Category>> mock = new Mock<IRepository<Category>>();
            mock.Setup(n => n.GetAllList())
                .Returns(new List<Category>()
                {
                    new Category()
                    {
                        CategoryId = 1,
                        CategoryName = "Some name",
                        Description = "Some description"
                    }
                });
            CategoriesController controller = new CategoriesController(mock.Object);

            IActionResult result = controller.Index();
            var model = ((ViewResult)result).ViewData.Model;
            var categories = (List<Category>)model;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<Category>>(model);
            Assert.AreEqual(1, categories.Count());
        }
    }
}
