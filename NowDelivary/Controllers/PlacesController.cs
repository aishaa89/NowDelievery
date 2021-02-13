using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.View_Model;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["AreaID"] = new SelectList(_context.Area, "ID", "AreaName");
            ViewData["PlaceCategoryID"] = new SelectList(_context.PlaceCategory, "ID", "PlaceCategoryName");
            return View();

        }

        public IActionResult GetFilteredPlaces(int areaID, int placeCategoryID)
        {
            PlaceVM vM = new PlaceVM(_context);
            return View(vM.FilterPlacesByAreaAndCategory(areaID,placeCategoryID));
        }

        public IActionResult Create()
        {
            ViewData["AreaID"] = new SelectList(_context.Area, "ID", "AreaName");
            ViewData["PlaceCategoryID"] = new SelectList(_context.PlaceCategory, "ID", "PlaceCategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Place.Add(place);
                _context.SaveChanges();
             
                return RedirectToAction("Index");
            }
            ViewData["AreaID"] = new SelectList(_context.Area, "ID", "AreaName", place.AreaID);
            ViewData["PlaceCategoryID"] = new SelectList(_context.PlaceCategory, "ID", "PlaceCategoryName", place.PlaceCategoryID);
            return View(place);
        }

        public IActionResult Edit(int?id)
        {
            if (id == null)
            {
               return NotFound();
            }
            ViewData["AreaID"] = new SelectList(_context.Area, "ID", "AreaName");
            ViewData["PlaceCategoryID"] = new SelectList(_context.PlaceCategory, "ID", "PlaceCategoryName");
            Place place = _context.Place.Find(id);
            return View(place);
        }

        [HttpPost]
            public IActionResult Edit(Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Place.Update(place);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AreaID"] = new SelectList(_context.Area, "ID", "AreaName", place.AreaID);
            ViewData["PlaceCategoryID"] = new SelectList(_context.PlaceCategory, "ID", "PlaceCategoryName", place.PlaceCategoryID);
            return View(place);

        }

           public IActionResult Delete(int id)
        {
            Place place = _context.Place.Find(id);
            if (place == null)
            {
                return NotFound();
            }
            _context.Place.Remove(place);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
