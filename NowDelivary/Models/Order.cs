using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }
        public string DelivarymanID { get; set; }
        public bool Status { get; set; }
        public double TotalPrice { get; set; }
        public double DelivarymanCost { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public CustomUser Customer { get; set; }

        public Order()
        {
            this.Status = false;
        }
    }
}
