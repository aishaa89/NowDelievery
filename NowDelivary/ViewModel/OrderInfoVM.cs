using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class OrderInfoVM
    {

  
        [ForeignKey("Place")]
        public int PlaceID { get; set; }

        public double DelivaryCost { get; set; }

        public IFormFile Image { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

    }
}
