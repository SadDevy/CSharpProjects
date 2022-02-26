using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoService.Models;

namespace AutoService.Controllers
{
    public class WorkSubkindController : Controller
    {
        private readonly AutoServiceContext context;

        public WorkSubkindController(AutoServiceContext context)
        {
            this.context = context;
        }

        public IActionResult GetAllWorkSubkinds()
        {
            return View(context.WorkSubkinds);
        }

        public IActionResult Add()
        {
            ViewBag.WorkKinds = context.WorkKinds;
            return View();
        }

        [HttpPost]
        public IActionResult Add(WorkSubkind workSubkind)
        {
            ViewBag.WorkKinds = context.WorkKinds;

            if (ModelState.IsValid)
            {
                context.WorkSubkinds.Add(workSubkind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkSubkinds", "WorkSubkind");
            }

            return View();
        }

        public IActionResult Change(int id)
        {
            ViewBag.WorkKinds = context.WorkKinds;
            return View(context.WorkSubkinds.Find(id));
        }

        [HttpPost]
        public IActionResult Change(WorkSubkind workSubkind)
        {
            ViewBag.WorkKinds = context.WorkKinds;

            if (ModelState.IsValid)
            {
                context.WorkSubkinds.Update(workSubkind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkSubkinds", "WorkSubkind");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewBag.WorkKinds = context.WorkKinds;
            return View(context.WorkSubkinds.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(WorkSubkind workSubkind)
        {
            ViewBag.WorkKinds = context.WorkKinds;

            if (ModelState.IsValid)
            {
                context.WorkSubkinds.Remove(workSubkind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkSubkinds", "WorkSubkind");
            }

            return View();
        }

        public async Task<IActionResult> GetWorkSubkind(int id)
        {
            WorkSubkind workSubkind = await context.WorkSubkinds.FindAsync(id);
            return View(workSubkind);
        }
    }
}
