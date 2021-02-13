using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class PlaceCategory
    {
        public int ID { get; set; }

        //[Index("IX_UniqueCategoryName",1,IsUnique =true)]
        public string PlaceCategoryName { get; set; }
    }
}
