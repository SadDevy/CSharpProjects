using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Introduction.Data;
using Introduction.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext context;
        private readonly ViewOptions options;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(NorthwindContext context, IOptions<ViewOptions> options, ILogger<ProductsController> logger)
        {
            this.context = context;
            this.options = options.Value;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await context.Products
                .Include(n => n.Supplier)
                .Include(n => n.Category)
                .ToListAsync();

            products = options.MaximumProducts == 0 ? products : products.Take(options.MaximumProducts);
            return View(products);
        }

        public IActionResult Add()
        {
            ViewBag.Suppliers = context.Suppliers.ToList();
            ViewBag.Categories = context.Categories.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued")] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Add(model);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suppliers = context.Suppliers.ToList();
            ViewBag.Categories = context.Categories.ToList();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Suppliers = context.Suppliers.ToList();
            ViewBag.Categories = context.Categories.ToList();

            Product product = await context.Products.FirstOrDefaultAsync(n => n.ProductId == id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ProductId, ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued")] Product model)
        {
            if (id != model.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(model);
                    await context.SaveChangesAsync();

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

            ViewBag.Suppliers = context.Suppliers.ToList();
            ViewBag.Categories = context.Categories.ToList();
            return View(model);
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(n => n.ProductId == id);
        }
    }
}
