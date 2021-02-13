using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly ApplicationDbContext Context;
        public AdminController(ApplicationDbContext _context , UserManager<CustomUser> _userManager)
        {
            Context = _context;
            userManager = _userManager;
        }

        public IActionResult AdminView()
        {
            return View();
        }

        public async Task<IActionResult> GetAllDelivarymen()
        {
            var delivarymen = await userManager.GetUsersInRoleAsync("Delivaryman");
            return View(delivarymen);
        }

        public IActionResult DelivarymanInformation(string delivarymanID)
        {
            DelivarymanVM vM = new DelivarymanVM(Context);
            return View(vM.DelivarymanInfo(delivarymanID));
        }

        public IActionResult IncomePage()
        {
            return View();
        }

        public IActionResult SetDelivaryCostByArea()
        {
            ViewData["CustomerArea"] = new SelectList(Context.Area.ToList(), "ID", "AreaName");
            ViewData["ShoopingArea"] = new SelectList(Context.Area.ToList(), "ID", "AreaName");
            return View();
        }

        [HttpPost]
        public IActionResult SetDelivaryCostByArea(DelivaryCost newDelivaryCost)
        {
            if (ModelState.IsValid)
            {
                Context.DelivaryCost.Add(newDelivaryCost);
                Context.SaveChanges();
                return Content("Cost added successfully");
            }
            return View(newDelivaryCost);
        }

        public IActionResult GetIncome(DateTime startDate , DateTime endDate)
        {
            IncomeVM incomeVM = new IncomeVM(Context);
            ViewBag.monthFrom = startDate.Month;
            ViewBag.monthTo = endDate.Month;
            ViewBag.year = startDate.Year;
            ViewBag.totalIncome = incomeVM.CompanyIncome(startDate, endDate);
            ViewBag.delivarymenCost = incomeVM.DelivarymenPrice(startDate, endDate);
            ViewBag.Profit = ViewBag.totalIncome - ViewBag.delivarymenCost;
            return View();
        }

    }
}