using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("webapi/api")]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet("[controller]")]
        [LogAction(true)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            return await _context.Categories
                .Select(n => new CategoryDTO() { CategoryId = n.CategoryId, CategoryName = n.CategoryName })
                .ToListAsync();
        }

        [HttpGet("category/{id}/image")]
        [Produces("image/jpeg")] //!!!
        public Stream GetImage(int id)
        {
            byte[] imageBytes = _context.Categories.FirstOrDefault(n => n.CategoryId == id)?.Picture;
            if (imageBytes == null)
                return null;

            MemoryStream model = new MemoryStream(imageBytes);
            return model;
        }

        [HttpGet("category/{id:int}")]
        [LogAction(true)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpGet("category/{name:alpha}")]
        [LogAction(true)]
        public async Task<ActionResult<Category>> GetCategory(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(n => n.CategoryName == name);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpGet("[controller]/{letter:alpha:length(1)}")]
        [LogAction(true)]
        public ActionResult<IEnumerable<Category>> GetCategoryStartedWith(char letter)
        {
            var categories = _context.Categories
                .ToList()
                .Where(n => n.CategoryName.StartsWith(letter))
                .AsParallel();

            if (categories == null)
                return NotFound();

            return categories;
        }

        [HttpGet("[controller]/{subString:minlength(2)}")]
        [LogAction(true)]
        public ActionResult<IEnumerable<Category>> GetCategoryStartedWith(string subString)
        {
            var categories = _context.Categories
                .ToList()
                .Where(n => n.CategoryName.Contains(subString))
                .AsParallel();

            if (categories == null)
                return NotFound();

            return categories;
        }

        [HttpPut("category/{id}/image")]
        public async Task<IActionResult> PutImage(int id, IFormFile file)
        {
            Category category = _context.Categories.FirstOrDefault(n => n.CategoryId == id);
            if (category == null)
                return NotFound();

            byte[] imageBytes = await GetImageBytes(file);
            category.Picture = imageBytes;
            _context.Categories.Update(category);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        private async Task<byte[]> GetImageBytes(IFormFile file)
        {
            byte[] buffer = new byte[file.Length];
            int readBytes;
            using (Stream stream = file.OpenReadStream())
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

        [HttpPut("category/{id}")]
        [LogAction(true)]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("category")]
        [LogAction(true)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        [HttpDelete("category/{id}")]
        [LogAction(true)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
