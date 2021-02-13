using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.View_Model;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    public class MenuCategoryController : Controller
    {

        ApplicationDbContext Context;
        public MenuCategoryController(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public IActionResult GetAll()
        {
            ViewData["area"] = new SelectList(Context.Area, "ID", "AreaName");
            return View();
        }

        public IActionResult GetAllAreaSupermarkets(int areaID)
        {
            PlaceVM vM = new PlaceVM(Context);
            return View(vM.GetAllSupermarket(areaID));
        }

        public IActionResult GetSupermarketSections(int supermarketID)
        {
            PlaceVM vM = new PlaceVM(Context);
            return View(vM.GetSupermarketMenuCategory(supermarketID));
        }

        public IActionResult CreateMenuCategory()
        {
            ViewData["placeID"] = new SelectList(Context.Place, "ID", "PlaceName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateMenuCategory(MenuCategory newmenucategory)
        {
            if (ModelState.IsValid)
            {
                Context.MenuCategorie.Add(newmenucategory);
                Context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            ViewData["placeID"] = new SelectList(Context.Place, "ID", "PlaceName",newmenucategory.PlaceID);
            return View(newmenucategory);
        }
        public async Task<IActionResult> Edite(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("GetAll");
            }
            ViewData["placeID"] = new SelectList(Context.Place, "ID", "PlaceName");

            var getmenuCategorydetails = await Context.MenuCategorie.FindAsync(id);
            return View(getmenuCategorydetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edite(MenuCategory MeCA)
        {
            if (ModelState.IsValid)
            {
                Context.Update(MeCA);
                await Context.SaveChangesAsync();
                return RedirectToAction("GetAll");

            }
            ViewData["placeID"] = new SelectList(Context.Place, "ID", "PlaceName", MeCA.PlaceID);

            return View(MeCA);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("GetAll");
            }
            var getmenuCategorydetails = await Context.MenuCategorie.FindAsync(id);
            return View(getmenuCategorydetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var getmenuCategorydetails = await Context.MenuCategorie.FindAsync(id);
            Context.MenuCategorie.Remove(getmenuCategorydetails);
            await Context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

    }
}