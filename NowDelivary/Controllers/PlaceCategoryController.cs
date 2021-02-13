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
    public class PlaceCategoryController : Controller
    {
        ApplicationDbContext _context;

        public PlaceCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View(_context.PlaceCategory.ToList());
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PlaceCategory mod)
        {
            if (ModelState.IsValid)
            {
                _context.PlaceCategory.Add(mod);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(mod);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PlaceCategory Mod = _context.PlaceCategory.Find(id);
            return View(Mod);
        }


        [HttpPost]
        public IActionResult Edit(PlaceCategory place)
        {
            if (ModelState.IsValid)
            {
                _context.PlaceCategory.Update(place);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
             return View(place);

        }

        public IActionResult Delete(int id)
        {
            PlaceCategory place = _context.PlaceCategory.Find(id);
            if (place == null)
            {
                return NotFound();
            }
            _context.PlaceCategory.Remove(place);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}