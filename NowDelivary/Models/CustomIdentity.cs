using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.Models
{
    public class CustomUser:IdentityUser
    {
        //[Index("IX_UniqueIdentityNumber", 1,IsUnique =true)]
        public int IdentityNumber { get; set; }
    }
}
