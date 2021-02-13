using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class DelivaryCost
    {
        public int ID { get; set; }
        public double Cost { get; set; }

        [ForeignKey("CustomerArea"),Column(Order =0)]
        public int CustomerAreaID { get; set; }

        [ForeignKey("ShoppingPlaceArea"), Column(Order = 1)]
        public int ShoppingPlaceAreaID { get; set; }

        public Area CustomerArea { get; set; }
        public Area ShoppingPlaceArea { get; set; }

    }
}
