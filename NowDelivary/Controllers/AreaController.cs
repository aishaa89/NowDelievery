using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowDelivary.Data;
using NowDelivary.Models;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AreaController : Controller
    {
        ApplicationDbContext Context;
        public AreaController(ApplicationDbContext _context)
        {
            Context = _context;
        }

        //[Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            return View(Context.Area.ToList());
        }

        public IActionResult CreateArea()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateArea(Area newArea)
        {
            if (ModelState.IsValid)
            {
                Context.Area.Add(newArea);
                Context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            return View(newArea);
        }


        public IActionResult EditArea(int id)
        {
            
            Area area = Context.Area.Find(id);
            return View(area);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArea(Area area)
        {
            if (ModelState.IsValid)
            {
                Context.Area.Update(area);
                Context.SaveChanges();
                return RedirectToAction("GetAll");
            }
             return View(area);

        }


        public IActionResult DeleteArea(int id)
        {
            Area area = Context.Area.Find(id);
            if (area == null)
            {
                return NotFound();
            }
            Context.Area.Remove(area);
            Context.SaveChanges();
            return RedirectToAction("GetAll");


        }
    }
}