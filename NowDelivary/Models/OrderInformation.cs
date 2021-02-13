using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class OrderInformation
    {
        public int ID { get; set; }

        [ForeignKey("Place")]
        public int PlaceID { get; set; }

        public double DelivaryCost { get; set; }
        public string OrderImage { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public Order Order { get; set; }
        public Place Place { get; set; }
    }
}
