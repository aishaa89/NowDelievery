using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    public class MenuController : Controller
    {

        ApplicationDbContext Context;
        public MenuController(ApplicationDbContext _context)
        {
            
            Context = _context;
        }

        public IActionResult GetAll()
        {
            MenuVM menuVM = new MenuVM(this.Context);
            return View(menuVM);
        }

        public IActionResult CreateMenu()
        {
            ViewData["menucatID"] = new SelectList(Context.MenuCategorie, "ID", "MenuCategoryType");
            return View();
        }

        [HttpPost]
        public IActionResult CreateMenu(Menu newmenu)
        {
            if (ModelState.IsValid)
            {
                Context.Menu.Add(newmenu);
                Context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            ViewData["menucatID"] = new SelectList(Context.MenuCategorie, "ID", "MenuCategoryType",newmenu.MenuCategoryID);

            return View(newmenu);
        }
        public async Task<IActionResult> Edite(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("GetAll");
            }
            ViewData["menucatID"] = new SelectList(Context.MenuCategorie, "ID", "MenuCategoryType");

            var getmenudetails = await Context.Menu.FindAsync(id);
            return View(getmenudetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edite(Menu Me)
        {
            if(ModelState.IsValid)
            {
                Context.Update(Me);
                await Context.SaveChangesAsync();
                return RedirectToAction("GetAll");

            }
            ViewData["menucatID"] = new SelectList(Context.MenuCategorie, "ID", "MenuCategoryType", Me.MenuCategoryID);
            return View(Me);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("GetAll");
            }
            var getmenudetails = await Context.Menu.FindAsync(id);
            return View(getmenudetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           
            var getmenudetails = await Context.Menu.FindAsync(id);
            Context.Menu.Remove(getmenudetails);
            await Context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}