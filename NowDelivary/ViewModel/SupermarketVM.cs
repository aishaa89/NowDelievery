using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class SupermarketVM
    {
        ApplicationDbContext Context;
        public SupermarketVM(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public IEnumerable<MenuCategory> GetMenuCategory(int supermarketID) => Context.MenuCategorie.Where(m => m.PlaceID == supermarketID).ToList();

        public List<MenuCategory> SupermarketsMenuCategory { get; set; }
    }
}
