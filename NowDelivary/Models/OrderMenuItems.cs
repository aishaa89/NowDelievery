using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class OrderMenuItems
    {
        public int ID { get; set; }

        [ForeignKey("OrderInformation")]
        //[Index("IX_UniqueOrderMenuItem",1,IsUnique =true)]
        public int OrderInformationID { get; set; }

        [ForeignKey("Menu")]
        //[Index("IX_UniqueOrderMenuItem", 2, IsUnique = true)]
        public int MenuID { get; set; }

        public int? MenuItemQuantity { get; set; }

        public OrderInformation OrderInformation { get; set; }
        public Menu Menu { get; set; }
    }
}
