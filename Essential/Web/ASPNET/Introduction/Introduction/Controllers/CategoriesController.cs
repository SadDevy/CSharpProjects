using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Introduction.Data;
using Introduction.Models;

namespace Introduction.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext context;

        public CategoriesController(NorthwindContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = context.Categories.ToList();
            return View(categories);
        }
    }
}
