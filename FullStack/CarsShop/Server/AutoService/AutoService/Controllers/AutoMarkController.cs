using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoService.Models;
using Microsoft.AspNetCore.Http;

namespace AutoService.Controllers
{
    public class AutoMarkController : Controller
    {
        private readonly AutoServiceContext context;

        public AutoMarkController(AutoServiceContext context)
        {
            this.context = context;
        }

        public IActionResult GetAllAutoMarks()
        {
            return View(context.AutoMarks);
        }

        [HttpGet]
        [Produces("image/jpeg")]
        public Stream GetImage(int id)
        {
            byte[] imageBytes = context.AutoMarks.First(n => n.Id == id).Image;
            return new MemoryStream(imageBytes);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AutoMark autoMark, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                autoMark.Image = image == null
                    ? null
                    : GetImageBytes(image);

                context.AutoMarks.Add(autoMark);
                context.SaveChanges();

                return RedirectToAction("GetAllAutoMarks", "AutoMark");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Change(int id)
        {
            AutoMark autoMark = context.AutoMarks.First(n => n.Id == id);
            return View(autoMark);
        }

        [HttpPost]
        public IActionResult Change(AutoMark autoMark, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                AutoMark source = context.AutoMarks.First(n => n.Id == autoMark.Id);

                if (image != null && source.Image == null)
                {
                    byte[] img = GetImageBytes(image);

                    source.Image = img;
                }

                source.Name = autoMark.Name;
                source.Description = autoMark.Description;
                source.DescriptionTitle = autoMark.DescriptionTitle;

                context.AutoMarks.Update(source);
                context.SaveChanges();

                return RedirectToAction("GetAllAutoMarks", "AutoMark");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(context.AutoMarks.First(n => n.Id == id));
        }

        [HttpPost]
        public IActionResult Delete(AutoMark autoMark)
        {
            if (ModelState.IsValid)
            {
                context.AutoMarks.Remove(autoMark);
                context.SaveChanges();

                return RedirectToAction("GetAllAutoMarks", "AutoMark");
            }

            return View();
        }


        private byte[] GetImageBytes(IFormFile uploadedFile)
        {
            byte[] buffer = new byte[uploadedFile.Length];
            int readBytes;
            using (Stream stream = uploadedFile.OpenReadStream())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    while ((readBytes = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memory.Write(buffer, 0, readBytes);
                    }

                    return memory.ToArray();
                }
            }
        }

        public async Task<IActionResult> GetAutoMark(int id)
        {
            AutoMark autoMark = await context.AutoMarks.FindAsync(id);
            return View(autoMark);
        }

        public IActionResult GetImageFile(int id)
        {
            byte[] imageBytes = context.AutoMarks.Find(id).Image;
            return File(imageBytes, "image/jpeg");
        }
    }
}
