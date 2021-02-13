using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class Place
    {
        public int ID { get; set; }

        [ForeignKey("Area")]
        //[Index("IX_UniquePlaceArea",1,IsUnique =true)]
        public int AreaID { get; set; }

        [ForeignKey("PlaceCategory")]
        //[Index("IX_UniquePlaceArea", 2, IsUnique = true)]
        public int PlaceCategoryID { get; set; }

        //[Index("IX_UniquePlaceArea", 3, IsUnique = true)]
        public string PlaceName { get; set; }

        public PlaceCategory PlaceCategory { get; set; }
        public Area Area { get; set; }
    }
}
