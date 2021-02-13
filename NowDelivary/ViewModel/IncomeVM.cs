using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class IncomeVM
    {

        ApplicationDbContext Context ;

        public IncomeVM(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public Area GetCustomerArea(string ID) => Context.CustomerAddresse.Where(m => m.CustomerID == ID).Select(a => a.Area).FirstOrDefault();

        // ---------------------------------------------------------------------------

        // Calculate Order TotalCost :

        public double DelivaryPlaceCost(int PlaceAreaID, int customerAreaID)
        {
            DelivaryCost cost = Context.DelivaryCost.FirstOrDefault(c => c.CustomerAreaID == customerAreaID && c.ShoppingPlaceAreaID == PlaceAreaID);
            if (cost != null)
                return cost.Cost;
            else
                return 5;
        }

        private Order GetCurrentOrder(int orderID) => Context.Order.Find(orderID);
        private IEnumerable<OrderInformation> GetOrderInfoByOrderID(int orderID) => Context.OrderInformation.Where(o => o.OrderID == orderID).ToList();

        public void OrderTotalCost(int orderID)
        {
            Order order = GetCurrentOrder(orderID);
            double totalPrice = 0;
            foreach (var item in GetOrderInfoByOrderID(order.ID))
            {
                totalPrice += item.DelivaryCost;
            }

            // order Price :
            order.TotalPrice = totalPrice;

            // delivaryman Price :
            DelivarymanPrice(order.ID);
        }

        // ---------------------------------------------------------------------------

        // Calculate Delivaryman Price :
        private void DelivarymanPrice(int orderID)
        {
            Order order = GetCurrentOrder(orderID);
            order.DelivarymanCost = order.TotalPrice * 15 / 100;
        }

        // ---------------------------------------------------------------------------

        // Calculate Company Income :

        public void AddCompanyIncome(DateTime dateFrom)
        {
            List<Order> monthOrders = Context.Order.Where(o=>o.Date.Month == dateFrom.Month && o.Date.Year == dateFrom.Year ).ToList();
            CompanyIncome companyIncome = new CompanyIncome()
            {
                DateFrom = dateFrom,
                DateTo = dateFrom.AddMonths(1),
                Income=0,
                DelivarymenCost=0,
                Profit=0
            };
            
            foreach (var order in monthOrders)
            {
                companyIncome.Income += order.TotalPrice;
                companyIncome.DelivarymenCost += order.DelivarymanCost;
            }
            companyIncome.Profit = companyIncome.Income - companyIncome.DelivarymenCost;
            Context.CompanyIncome.Add(companyIncome);
            Context.SaveChanges();
        }


        private IEnumerable<Order> CurrentOrders(DateTime date,DateTime dateTo) => Context.Order.Where(o => o.Date.Month >= date.Month && o.Date.Year == dateTo.Year && o.Date.Month <= dateTo.Month);

        public double CompanyIncome(DateTime DateFrom , DateTime DateTo)
        {
            double TotalIncome = 0;
            foreach (var item in CurrentOrders(DateFrom, DateTo))
            {
                TotalIncome += item.TotalPrice;
            }
            return TotalIncome;
        }
        public double DelivarymenPrice(DateTime DateFrom, DateTime DateTo)
        {
            double delivarymenCost = 0;
            foreach (var item in CurrentOrders(DateFrom, DateTo))
            {
                delivarymenCost += item.DelivarymanCost;
            }
            return delivarymenCost;
        }

    }
}
