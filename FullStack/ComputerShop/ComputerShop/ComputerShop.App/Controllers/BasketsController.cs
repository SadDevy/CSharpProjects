using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComputerShop.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.App.Controllers
{
    public class BasketsController : Controller
    {
        private readonly ComputerShopContext context;

        public BasketsController(ComputerShopContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GetAllGoods()
        {
            string userName = User.FindFirst(n => n.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            int userId = context.Users.First(n => n.Login == userName).Id;

            IEnumerable<int?> userBasketElementsId =
                await context.Baskets.Where(n => n.UserId == userId)
                    .Select(n => n.BasketEmentId)
                    .ToListAsync();

            return View(context.BasketElements.Where(n => userBasketElementsId.Contains(n.Id)));
        }

        public async Task<IActionResult> DeleteGoods(int basketElementId)
        {
            string userName = User.FindFirst(n => n.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            int userId = context.Users.First(n => n.Login == userName).Id;

            Basket basket = context.Baskets.First(n => n.UserId == userId && n.BasketEmentId == basketElementId);

            context.Baskets.Remove(basket);
            await context.SaveChangesAsync();

            BasketElement element = context.BasketElements.First(n => n.Id == basketElementId);

            context.BasketElements.Remove(element);
            await context.SaveChangesAsync();

            return RedirectToAction("GetAllGoods", "Baskets");
        }

        public async Task<IActionResult> DeleteAllGoods()
        {
            string userName = User.FindFirst(n => n.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            int userId = context.Users.First(n => n.Login == userName).Id;

            IEnumerable<Basket> baskets =
                await context.Baskets
                    .Where(n => n.UserId == userId)
                    .ToArrayAsync();

            context.Baskets.RemoveRange(baskets);
            await context.SaveChangesAsync();

            IEnumerable<BasketElement> elements =
                await context.BasketElements
                    .Where(n => baskets.Select(n => n.BasketEmentId).Contains(n.Id))
                    .ToListAsync();

            context.BasketElements.RemoveRange(elements);
            await context.SaveChangesAsync();

            return RedirectToAction("GetAllGoods", "Baskets");
        }
    }
}
