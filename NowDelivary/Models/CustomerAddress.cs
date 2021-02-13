using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class CustomerAddress
    {
        public int ID { get; set; }

        //[Index("IX_UniqueAddressArea", 1, IsUnique = true)]
        public string Description { get; set; }
        public string SpecialMark { get; set; }

        [ForeignKey("Area")]
        //[Index("IX_UniqueAddressArea", 2, IsUnique = true)]
        public int AreaID { get; set; }

        [ForeignKey("Customer")]
        //[Index("IX_UniqueAddressArea", 3, IsUnique = true)]
        public string CustomerID { get; set; }

        public CustomUser Customer { get; set; }
        public Area Area { get; set; }
    }
}
