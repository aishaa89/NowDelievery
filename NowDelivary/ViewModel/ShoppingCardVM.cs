using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class ShoppingCardVM
    {
        public ApplicationDbContext Context;
        public ShoppingCardVM(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public Order CurrentNotDeliveredOrder(string customerID) => Context.Order.FirstOrDefault(o => o.Date.Hour == DateTime.Now.Hour && o.CustomerID == customerID && o.Status == false);

        public List<OrderInformation> CurrentOrderInformation(int orderID)
        {
            return Context.OrderInformation.Where(o => o.OrderID == orderID && o.Order.Date.Hour == DateTime.Now.Hour).ToList();
        }

        public List<OrderMenuItems> orderItems(int orderInfoID)
        {
            return Context.OrderMenuItems.Where(m => m.OrderInformationID == orderInfoID).ToList();
        }

        public List<Menu> ShoppingItems(int orderInfoID)
        {
            List<Menu> menu = new List<Menu>();

            foreach (var item in orderItems(orderInfoID))
            {
                menu.Add(Context.Menu.Find(item.MenuID));
            }

            return menu;
        }

    }
}
