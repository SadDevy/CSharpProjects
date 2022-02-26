using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Introduction.Models;
using Introduction.Data.Repositories;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using Introduction.Data;

namespace Introduction.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> context;

        public CategoriesController(IRepository<Category> context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = context.GetAllList();
            return View(categories);
        }

        [HttpGet]
        [Produces("image/jpeg")]
        public Stream GetImage(int id)
        {
            byte[] imageBytes = context.GetElement(id).Picture;

            MemoryStream model = new MemoryStream(imageBytes);
            return model;
        }

        public IActionResult Edit(int id)
        {
            Category category = context.GetElement(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,
            [Bind("CategoryId, CategoryName, Description")] Category model,
            IFormFile uploadedFile)
        {
            if (id != model.CategoryId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] image = await GetImageBytes(uploadedFile);
                    model.Picture = image;

                    context.Update(model);
                    context.Save();
                }
                catch (Exception)
                {
                    if (!CategoryExists(id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private async Task<byte[]> GetImageBytes(IFormFile uploadedFile)
        {
            byte[] buffer = new byte[uploadedFile.Length];
            int readBytes;
            using (Stream stream = uploadedFile.OpenReadStream())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    while ((readBytes = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await memory.WriteAsync(buffer, 0, readBytes);
                    }
                    return memory.ToArray();
                }
            }
        }

        private bool CategoryExists(int id)
        {
            return context.GetAllList().Any(n => n.CategoryId == id);
        }
    }
}
