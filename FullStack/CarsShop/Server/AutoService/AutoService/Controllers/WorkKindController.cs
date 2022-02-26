using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoService.Models;

namespace AutoService.Controllers
{
    public class WorkKindController : Controller
    {
        private readonly AutoServiceContext context;

        public WorkKindController(AutoServiceContext context)
        {
            this.context = context;
        }

        public IActionResult GetAllWorkKinds()
        {
            return View(context.WorkKinds);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(WorkKind workKind)
        {
            if (ModelState.IsValid)
            {
                context.WorkKinds.Add(workKind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkKinds", "WorkKind");
            }

            return View();
        }

        public IActionResult Change(int id)
        {
            return View(context.WorkKinds.Find(id));
        }

        [HttpPost]
        public IActionResult Change(WorkKind workKind)
        {
            if (ModelState.IsValid)
            {
                context.WorkKinds.Update(workKind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkKinds", "WorkKind");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            return View(context.WorkKinds.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(WorkKind workKind)
        {
            if (ModelState.IsValid)
            {
                context.WorkKinds.Remove(workKind);
                context.SaveChanges();

                return RedirectToAction("GetAllWorkKinds", "WorkKind");
            }

            return View();
        }

        public async Task<IActionResult> GetWorkKind(int id)
        {
            WorkKind workKind = await context.WorkKinds.FindAsync(id);
            return View(workKind);
        }
    }
}
