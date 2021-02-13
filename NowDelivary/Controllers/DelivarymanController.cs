using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Delivaryman")]
    public class DelivarymanController : Controller
    {
        private readonly ApplicationDbContext Context;

        public DelivarymanController(ApplicationDbContext _context)
        {
            Context = _context;
        }

        private string GetLoginDelivaryman()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentLoginUser currentLoginUser = new CurrentLoginUser();
            currentLoginUser.CurrentUserID = userID;

            return userID;
        }

        public IActionResult GetNotDeliveredOrders()
        {
            DelivarymanVM DelivarymanVM = new DelivarymanVM(Context);
            ViewData["delivaryman"] = GetLoginDelivaryman();
            return View(DelivarymanVM);
        }

        public IActionResult DeliveredOrder(int orderID)
        {
            Order deliveredOrder = Context.Order.Find(orderID);
            IncomeVM incomeVM = new IncomeVM(Context);
            deliveredOrder.Status = true;
            Context.SaveChanges();

            // add company income :
            CompanyIncome companyIncome = Context.CompanyIncome.FirstOrDefault(i => i.DateFrom.Month == deliveredOrder.Date.Month && i.DateFrom.Year == deliveredOrder.Date.Year);
            if (companyIncome != null)
            {
                companyIncome.Income += deliveredOrder.TotalPrice;
                companyIncome.DelivarymenCost += deliveredOrder.DelivarymanCost;
                companyIncome.Profit = companyIncome.Income - companyIncome.DelivarymenCost;
                Context.SaveChanges();
            }
            else
            {
                incomeVM.AddCompanyIncome(deliveredOrder.Date);
            }

            return RedirectToAction("GetNotDeliveredOrders");
        }

        public IActionResult CustomerInformation (string customerID)
        {
            ViewData["customer"] = customerID;

            DelivarymanVM delivarymanVM = new DelivarymanVM(Context);
            return View(delivarymanVM);
        }

        public IActionResult GetOrderItems(int orderInfoID)
        {
            ViewBag.orderInfo = orderInfoID;

            DelivarymanVM delivarymanVM = new DelivarymanVM(Context);
            return View(delivarymanVM);
        }

    }
}