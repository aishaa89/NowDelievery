using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NowDelivary.Data;
using NowDelivary.Models;
using NowDelivary.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NowDelivary.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderPharmController : Controller
    {

        ApplicationDbContext Context;
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<CustomUser> userManager;

        [Obsolete]
        public OrderPharmController( IHostingEnvironment IHostingEnvironment, ApplicationDbContext _Context, UserManager<CustomUser> _userMnager)
        {
            _environment = IHostingEnvironment;
            Context = _Context;
            userManager = _userMnager;
      
        }
        public IActionResult ChooseAreaForpharmacy()
        {
            ViewData["GetArea"] = new SelectList(Context.Area.ToList(), "ID", "AreaName");
            return View();
        }


        public IActionResult GetAllPharmacytsbyArea(int AreaID)
        {
            ViewData["allpharm"] = new SelectList(Context.Place.Where(p => p.PlaceCategory.PlaceCategoryName == "Pharmacy" && p.AreaID == AreaID), "ID", "PlaceName");
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult GetAllPharmacytsbyArea(OrderInfoVM orderInfoVM)
        {
            IncomeVM vM = new IncomeVM(this.Context);
            if (ModelState.IsValid)
            {
                Order checkExistOrder = Context.Order.FirstOrDefault(o => o.Date.Hour == DateTime.Now.Hour && o.CustomerID == GetLoginCustomer());

                string uniqueFileName = null;

                if (checkExistOrder != null)
                {
                    OrderInformation checkExistOrderInfo = Context.OrderInformation.FirstOrDefault(o => o.PlaceID == orderInfoVM.PlaceID && o.OrderID == checkExistOrder.ID);

                    if (checkExistOrderInfo == null)
                    {
                        if (orderInfoVM.Image != null)
                        {
                            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + orderInfoVM.Image.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            orderInfoVM.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                        OrderInformation newOrder = new OrderInformation();
                        newOrder.PlaceID = orderInfoVM.PlaceID;
                        newOrder.OrderImage = uniqueFileName;
                        newOrder.DelivaryCost = vM.DelivaryPlaceCost(newOrder.PlaceID, vM.GetCustomerArea(GetLoginCustomer()).ID);
                        newOrder.OrderID = checkExistOrder.ID;

                        Context.OrderInformation.Add(newOrder);
                        Context.SaveChanges();

                    }
                }
                else
                {
                    if (orderInfoVM.Image != null)
                    {
                        string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + orderInfoVM.Image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        orderInfoVM.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    checkExistOrder = SetNewOrder();
                    Context.Order.Add(checkExistOrder);
                    Context.SaveChanges();

                    OrderInformation newOrderInfo = SetNewOrderInfo(orderInfoVM.PlaceID, checkExistOrder.ID, uniqueFileName);
                    Context.OrderInformation.Add(newOrderInfo);
                    Context.SaveChanges();

                }

                //updateTotalcost(checkExistOrder.ID);
                return RedirectToAction("ShoppingCard", "Customer");


            }

            return View();

        }

        private Order SetNewOrder()
        {
            Order order = new Order();
            order.CustomerID = GetLoginCustomer().ToString();
            order.Time = DateTime.Now.Hour + 1; // order will delivered withen an hour from request order
            order.Date = DateTime.Now;
            order.DelivarymanID = SelectRandomDelivaryman().Result.Id;

            // initial states ....
            order.Status = false;
            order.DelivarymanCost = 0;
            order.TotalPrice = 0;

            return order;
        }

        private OrderInformation SetNewOrderInfo(int placeID, int orderID,string img)
        {
            IncomeVM vM = new IncomeVM(this.Context);
            OrderInformation newOrderInfo = new OrderInformation();
            newOrderInfo.PlaceID = placeID;
            newOrderInfo.DelivaryCost = vM.DelivaryPlaceCost(placeID, vM.GetCustomerArea(GetLoginCustomer()).ID);
            newOrderInfo.OrderID = orderID;
            newOrderInfo.OrderImage = img;

            return newOrderInfo;
        }

        public void updateTotalcost(int orderID)
        {
            Order currentOrder = Context.Order.Find(orderID);
            IncomeVM incomeVM = new IncomeVM(Context);
            if (currentOrder != null)
            {
                incomeVM.OrderTotalCost(orderID);
                Context.SaveChanges();
            }
        }

        private async Task<CustomUser> SelectRandomDelivaryman()
        {
            var delivarymen = await userManager.GetUsersInRoleAsync("Delivaryman");
            Random randomDelivaryman = new Random();
            return delivarymen[randomDelivaryman.Next(delivarymen.Count)];
        }




        private string GetLoginCustomer()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentLoginUser currentLoginUser = new CurrentLoginUser();
            currentLoginUser.CurrentUserID = userID;

            return userID;
        }
    }
}