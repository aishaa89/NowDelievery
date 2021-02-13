using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class MenuCategory
    {
        public int ID { get; set; }

        //[Index("IX_UniqueMenuCategory",1,IsUnique =true)]
        public string MenuCategoryType { get; set; }

        [ForeignKey("Place")]
        //[Index("IX_UniqueMenuCategory", 2, IsUnique = true)]
        public int PlaceID { get; set; }
        public Place Place { get; set; }
    }
}
