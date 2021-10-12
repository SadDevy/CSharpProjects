using System;
using System.Collections.Generic;
using System.Linq;
using Introduction.Data;
using Introduction.Data.Repositories;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> context;
        private readonly IRepository<Supplier> suppliersRepository;
        private readonly IRepository<Category> categoriesRepository;

        private readonly ViewOptions options;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(
            IRepository<Product> context,
            IRepository<Supplier> suppliersRepository,
            IRepository<Category> categoriesRepository,
            IOptions<ViewOptions> options, ILogger<ProductsController> logger)
        {
            this.context = context;
            this.suppliersRepository = suppliersRepository;
            this.categoriesRepository = categoriesRepository;

            this.options = options.Value;
            this.logger = logger;
        }

        [LogAction(true)]
        public IActionResult Index()
        {
            IEnumerable<Product> products = context.GetAllList();

            products = options.MaximumProducts == 0 ? products : products.Take(options.MaximumProducts);
            return View(products);
        }

        public IActionResult Add()
        {
            ViewBag.Suppliers = suppliersRepository.GetAllList();
            ViewBag.Categories = categoriesRepository.GetAllList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued")] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Add(model);
                context.Save();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suppliers = suppliersRepository.GetAllList();
            ViewBag.Categories = categoriesRepository.GetAllList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Suppliers = suppliersRepository.GetAllList();
            ViewBag.Categories = categoriesRepository.GetAllList();

            Product product = context.GetElement(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("ProductId, ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued")] Product model)
        {
            if (id != model.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(model);
                    context.Save();

                    //throw new Exception("new exception"); //Thrown exception
                }
                catch (Exception ex)
                {
                    logger.LogError($"An error occurred: {ex}.");

                    if (!ProductExists(id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suppliers = suppliersRepository.GetAllList();
            ViewBag.Categories = categoriesRepository.GetAllList();
            return View(model);
        }

        private bool ProductExists(int id)
        {
            return context.GetAllList().Any(n => n.ProductId == id);
        }
    }
}
