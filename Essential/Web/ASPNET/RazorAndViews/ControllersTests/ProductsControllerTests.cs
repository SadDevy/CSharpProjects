using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Introduction.Controllers;
using Introduction.Data;
using Introduction.Data.Repositories;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControllersTests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IRepository<Product>> mockRepositoryProduct;
        private Mock<IRepository<Supplier>> mockRepositorySupplier;
        private Mock<IRepository<Category>> mockRepositoryCategory;
        private Mock<IOptions<ViewOptions>> mockOptions;
        private Mock<ILogger<ProductsController>> mockLogger;

        [SetUp]
        public void SetUp()
        {
            mockRepositoryProduct = new Mock<IRepository<Product>>();
            mockRepositoryProduct.Setup(n => n.GetAllList())
                .Returns(new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Some name"
                    }
                });
            mockRepositoryProduct.Setup(n => n.GetElement(1))
                .Returns(new Product()
                {
                    ProductId = 1,
                    ProductName = "Some name"
                });

            mockRepositorySupplier = new Mock<IRepository<Supplier>>();
            mockRepositoryCategory = new Mock<IRepository<Category>>();

            mockOptions = new Mock<IOptions<ViewOptions>>();
            mockOptions.Setup(n => n.Value)
                .Returns(new ViewOptions() { MaximumProducts = 0 });

            mockLogger = new Mock<ILogger<ProductsController>>();
        }

        [Test]
        public void Index_ReturnViewResult_WithProductsList()
        {
            ProductsController controller = new ProductsController(mockRepositoryProduct.Object, mockRepositorySupplier.Object, mockRepositoryCategory.Object, mockOptions.Object, mockLogger.Object);

            IActionResult result = controller.Index();
            var model = ((ViewResult)result).ViewData.Model;
            var products = (List<Product>)model;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<Product>>(model);
            Assert.AreEqual(1, products.Count());
        }

        [Test]
        public void AddGet_ReturnViewResult_WithoutProductList()
        {
            ProductsController controller = new ProductsController(mockRepositoryProduct.Object, mockRepositorySupplier.Object, mockRepositoryCategory.Object, mockOptions.Object, mockLogger.Object);

            IActionResult result = controller.Add();
            var model = ((ViewResult)result).ViewData.Model;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNull(model);
        }

        [Test]
        public void AddPost_ReturnViewResult_WithProduct()
        {
            ProductsController controller = new ProductsController(mockRepositoryProduct.Object, mockRepositorySupplier.Object, mockRepositoryCategory.Object, mockOptions.Object, mockLogger.Object);

            controller.ModelState.AddModelError("ProductName", "Required");
            IActionResult result = controller.Add(new Product()
            {
                ProductId = 2
            });
            var model = ((ViewResult)result).ViewData.Model;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<Product>(model);
        }

        [Test]
        public void EditGet_ReturnViewResult_WithProduct()
        {
            ProductsController controller = new ProductsController(mockRepositoryProduct.Object, mockRepositorySupplier.Object, mockRepositoryCategory.Object, mockOptions.Object, mockLogger.Object);

            IActionResult result = controller.Edit(1);
            var model = ((ViewResult)result).ViewData.Model;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<Product>(model);
        }

        [Test]
        public void EditPost_ReturnViewResult_WithProductList()
        {
            ProductsController controller = new ProductsController(mockRepositoryProduct.Object, mockRepositorySupplier.Object, mockRepositoryCategory.Object, mockOptions.Object, mockLogger.Object);

            controller.ModelState.AddModelError("ProductName", "Required");
            IActionResult result = controller.Edit(1, new Product()
            {
                ProductId = 2
            });
            var model = ((NotFoundResult)result);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
