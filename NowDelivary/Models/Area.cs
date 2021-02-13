using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class Area
    {
        public int ID { get; set; }

        //[Index("IX_UniqueArea",1,IsUnique =true)]
        [Required]
        public string AreaName { get; set; }
    }
}
