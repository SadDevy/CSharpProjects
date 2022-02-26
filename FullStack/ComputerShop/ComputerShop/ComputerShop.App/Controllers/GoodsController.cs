using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComputerShop.App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.App.Controllers
{
    public class GoodsController : Controller
    {
        private readonly ComputerShopContext context;

        public GoodsController(ComputerShopContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GetAllGoods(int subcatalogId)
        {
            IEnumerable<Good> goods = await context.Goods
                .Where(n => n.SubcatalogId == subcatalogId)
                .ToListAsync();

            return View(goods);
        }

        public async Task<IActionResult> GetGoods(int id)
        {
            Good goods = await context.Goods.FindAsync(id);

            return View(goods);
        }

        public async Task<IActionResult> GetAllGoodsAsTable()
        {
            IEnumerable<Good> goods = await context.Goods.ToListAsync();

            return View(goods);
        }

        [HttpGet]
        [Produces("image/jpeg")]
        public Stream GetImage(int id)
        {
            byte[] imageBytes = context.Goods.First(n => n.Id == id).Picture;
            return new MemoryStream(imageBytes);
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

        public async Task<IActionResult> GetImageFile(int id)
        {
            byte[] imageBytes = (await context.Goods.FirstAsync(n => n.Id == id)).Picture;
            return File(imageBytes, "image/jpeg");
        }

        public IActionResult Add()
        {
            ViewBag.Subcatalogs = context.Subcatalogs;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Good goods, IFormFile image)
        {
            ViewBag.Subcatalogs = context.Subcatalogs;
            if (ModelState.IsValid)
            {
                goods.Picture = image == null
                    ? null
                    : GetImageBytes(image);

                context.Goods.Add(goods);
                context.SaveChanges();

                return RedirectToAction("GetAllGoodsAsTable", "Goods");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Change(int id)
        {
            Good goods = context.Goods.First(n => n.Id == id);
            ViewBag.Subcatalogs = context.Subcatalogs;
            return View(goods);
        }

        [HttpPost]
        public IActionResult Change(Good goods, IFormFile image)
        {
            ViewBag.Subcatalogs = context.Subcatalogs;
            if (ModelState.IsValid)
            {
                Good source = context.Goods.First(n => n.Id == goods.Id);

                if (image != null)
                {
                    byte[] img = GetImageBytes(image);

                    source.Picture = img;
                }

                source.Name = goods.Name;
                source.Description = goods.Description;
                source.Count = goods.Count;
                source.Price = goods.Price;
                source.SubcatalogId = goods.SubcatalogId;

                context.Goods.Update(source);
                context.SaveChanges();

                return RedirectToAction("GetAllGoodsAsTable", "Goods");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Good goods = context.Goods.Find(id);

            context.Goods.Remove(goods);
            context.SaveChanges();

            return RedirectToAction("GetAllGoodsAsTable", "Goods");
        }

        [HttpGet]
        public IActionResult AddToBasket(int goodsId)
        {
            string userName = User.FindFirst(n => n.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            int userId = context.Users.First(n => n.Login == userName).Id;

            context.BasketElements.Add(new BasketElement()
            {
                GoodsId = goodsId,
                Count = 1
            });

            context.SaveChanges();

            int basketElementId = context.BasketElements.Max(n => n.Id);
            context.Baskets.Add(new Basket()
            {
                UserId = userId,
                BasketEmentId = basketElementId
            });

            context.SaveChanges();

            int subcatalogId = context.Goods.Find(goodsId).SubcatalogId;
            return RedirectToAction("GetAllGoods", "Goods", new { subcatalogId = subcatalogId });
        }
    }
}
