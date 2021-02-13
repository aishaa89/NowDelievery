using NowDelivary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using NowDelivary.Models;

namespace NowDelivary.ViewModel
{
    public class DelivarymanVM
    {
        private readonly ApplicationDbContext Context;
        public DelivarymanVM(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public IEnumerable<Order> GetAllOrders(string DelivarymanID) => Context.Order.Where(o => o.DelivarymanID == DelivarymanID).ToList();

        public IEnumerable<Order> GetDeliveredOrders(string DelivarymanID) => GetAllOrders(DelivarymanID).Where(o => o.Status == true).ToList();

        public IEnumerable<Order> GetNotDeliveredOrders(string DelivarymanID) => GetAllOrders(DelivarymanID).Where(o => o.Status == false).ToList();

        public Area GetAreaByAddress(int addressID) => Context.CustomerAddresse.Where(a => a.ID == addressID).Select(a => a.Area).FirstOrDefault();

        public CustomUser DelivarymanInfo(string delivarymanID) => Context.Users.Find(delivarymanID);

        public CustomUser GetCustomerInformation(string customerID) => Context.Users.Find(customerID);

        public CustomerAddress GetCustomerAddress(string customerID) => Context.CustomerAddresse.FirstOrDefault(a => a.CustomerID == customerID);

        // ------------------------------------------------------------

        public IEnumerable<OrderInformation> GetOrderInformation(int orderID) => Context.OrderInformation.Where(o => o.OrderID == orderID).ToList();

        public Place GetPlaceByOrderInformationID(int orderInfoID) => Context.OrderInformation.Where(o => o.ID == orderInfoID).Select(o=>o.Place).FirstOrDefault();

        public Area GetAreaByPlaceID(int placeID) => Context.Place.Where(p => p.ID == placeID).Select(p=>p.Area).FirstOrDefault();

        public OrderInformation GetOrderInformationByID(int orderInfoID) => Context.OrderInformation.Find(orderInfoID);

        public IEnumerable<OrderMenuItems> GetOrderItems(int orderInfo) => Context.OrderMenuItems.Where(o => o.OrderInformationID == orderInfo);

        public Menu GetMenuItems(int menuID) => Context.Menu.Find(menuID);

    }
}
