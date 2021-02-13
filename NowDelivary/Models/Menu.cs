using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class Menu
    {
        public int ID { get; set; }

        //[Index("IX_UniqueMenuItem",1,IsUnique =true)]
        public string MenuItem { get; set; }

        //[Index("IX_UniqueMenuItem", 2, IsUnique = true)]
        public double ItemPrice { get; set; }

        [ForeignKey("MenuCategory")]
        public int MenuCategoryID { get; set; }
        public MenuCategory MenuCategory { get; set; }
    }
}
