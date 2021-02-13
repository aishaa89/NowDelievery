using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.View_Model;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        ApplicationDbContext Context;
        private readonly UserManager<CustomUser> userManager;
       
        public CustomerController(ApplicationDbContext _context, UserManager<CustomUser> _userMnager)
        {
            Context = _context;
            userManager = _userMnager;
         

        }

        public IActionResult ChooseAreaForSupermarket()
        {
            ViewData["GetArea"] = new SelectList(Context.Area.ToList(),"ID","AreaName");
            return View();
        }

        public IActionResult GetAllSupermarketsbyArea(int AreaID)
        {
            PlaceVM placeVM = new PlaceVM(this.Context);
            return View(placeVM.GetAllSupermarket(AreaID));
        }

        public IActionResult GetMenuCategory(int supermarketID)
        {
            ViewData["menuCat"] = new SelectList(Context.MenuCategorie.Where(m => m.PlaceID == supermarketID), "ID", "MenuCategoryType");
            return View();
        }

        public IActionResult GetMenu(int menuCategotyID)
        {
            PlaceVM placeVM = new PlaceVM(this.Context);
            ViewBag.MenuCatID = menuCategotyID;
            ViewBag.menuCat = menuCategotyID;

            return View(placeVM);
        }

        public IActionResult BuyItem(int menuID, int itemQuantity, int supermarketID)
        {
            IncomeVM vM = new IncomeVM(Context);

            Order checkExistOrder = Context.Order.FirstOrDefault(o => o.Date.Hour == DateTime.Now.Hour && o.CustomerID == GetLoginCustomer());

            if (checkExistOrder != null)
            {
                OrderInformation checkExistOrderInfo = Context.OrderInformation.FirstOrDefault(o => o.PlaceID == supermarketID && o.OrderID == checkExistOrder.ID);

                if (checkExistOrderInfo == null)
                {
                    OrderInformation newOrderInfo = new OrderInformation();
                    newOrderInfo.PlaceID = supermarketID;

                    newOrderInfo.DelivaryCost = vM.DelivaryPlaceCost(supermarketID, vM.GetCustomerArea(GetLoginCustomer()).ID);
                    newOrderInfo.OrderID = checkExistOrder.ID;
                    Context.OrderInformation.Add(newOrderInfo);
                    Context.SaveChanges();

                    OrderMenuItems menuItem = new OrderMenuItems();
                    menuItem.MenuID = menuID;
                    menuItem.MenuItemQuantity = itemQuantity;
                    menuItem.OrderInformationID = newOrderInfo.ID;
                    Context.OrderMenuItems.Add(menuItem);
                    Context.SaveChanges();
                }
                else
                {
                    OrderMenuItems menuItem = new OrderMenuItems();
                    menuItem.MenuID = menuID;
                    menuItem.MenuItemQuantity = itemQuantity;
                    menuItem.OrderInformationID = checkExistOrderInfo.ID;
                    Context.OrderMenuItems.Add(menuItem);
                    Context.SaveChanges();
                }
            }
            else
            {
                checkExistOrder = SetNewOrder();
                Context.Order.Add(checkExistOrder);
                Context.SaveChanges();

                OrderInformation newOrderInfo = SetNewOrderInfo(supermarketID, checkExistOrder.ID);
                Context.OrderInformation.Add(newOrderInfo);
                Context.SaveChanges();

                OrderMenuItems newOrderMenuItems = SetNewMenuItem(menuID, newOrderInfo.ID, itemQuantity);
                Context.OrderMenuItems.Add(newOrderMenuItems);
                Context.SaveChanges();
            }

            return Content("Added to Shopping Card");
        }

        public IActionResult ShoppingCard()
        {
            ShoppingCardVM shoppingvm = new ShoppingCardVM(Context);
            if (shoppingvm.CurrentNotDeliveredOrder(GetLoginCustomer()) != null)
            {
                ViewBag.orderID = shoppingvm.CurrentNotDeliveredOrder(GetLoginCustomer()).ID;
            }
            ShoppingCardVM vM = new ShoppingCardVM(Context);
            return View(vM);
        }

        public IActionResult SubmitOrder(int orderID)
        {
            Order currentOrder = Context.Order.Find(orderID);
            IncomeVM incomeVM = new IncomeVM(Context);
            if (currentOrder != null)
            {
                incomeVM.OrderTotalCost(orderID);
                Context.SaveChanges();
            }
            
            //CompanyIncome companyIncome = Context.CompanyIncome.FirstOrDefault(i=>i.DateFrom.Month == currentOrder.Date.Month && i.DateFrom.Year == currentOrder.Date.Year);
            //if (companyIncome != null)
            //{
            //    companyIncome.Income += currentOrder.TotalPrice;
            //    companyIncome.DelivarymenCost += currentOrder.DelivarymanCost;
            //    companyIncome.Profit = companyIncome.Income - companyIncome.DelivarymenCost;
            //    Context.SaveChanges();
            //}
            //else
            //{
            //    incomeVM.AddCompanyIncome(currentOrder.Date);
            //}

            return RedirectToAction("Index","Home");
        }

        private Order SetNewOrder()
        {
            Order order = new Order();
            order.CustomerID = GetLoginCustomer().ToString();
            order.Time = DateTime.Now.Hour+1; // order will delivered withen an hour from request order
            order.Date = DateTime.Now;
            order.DelivarymanID = SelectRandomDelivaryman().Result.Id;

            // initial states ....
            order.Status = false;
            order.DelivarymanCost = 0;
            order.TotalPrice = 0;
            
            
            return order;
        }

        private OrderInformation SetNewOrderInfo(int placeID , int orderID)
        {
            IncomeVM vM = new IncomeVM(Context);
            OrderInformation newOrderInfo = new OrderInformation();
            newOrderInfo.PlaceID = placeID;
            newOrderInfo.DelivaryCost = vM.DelivaryPlaceCost(placeID, vM.GetCustomerArea(GetLoginCustomer()).ID);
            newOrderInfo.OrderID = orderID;
            return newOrderInfo;
        }

        private OrderMenuItems SetNewMenuItem(int menuItemID, int orderInfoID , int menuItemQuantity)
        {
            OrderMenuItems newMenuItem = new OrderMenuItems();
            newMenuItem.MenuID = menuItemID;
            newMenuItem.MenuItemQuantity = menuItemQuantity;
            newMenuItem.OrderInformationID = orderInfoID;

            return newMenuItem;
        }

        private string GetLoginCustomer()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentLoginUser currentLoginUser = new CurrentLoginUser();
            currentLoginUser.CurrentUserID = userID;

            return userID;
        }

        private async Task<CustomUser> SelectRandomDelivaryman()
        {
            var delivarymen = await userManager.GetUsersInRoleAsync("Delivaryman");
            Random randomDelivaryman = new Random();
            return delivarymen[randomDelivaryman.Next(delivarymen.Count)];
        }
    }
}