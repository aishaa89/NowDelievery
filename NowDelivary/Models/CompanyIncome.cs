using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class CompanyIncome
    {
        public int ID { get; set; }
        public double Income { get; set; }
        public double DelivarymenCost { get; set; }
        public double Profit { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
