using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class MenCatVM
    {
        ApplicationDbContext Context;
        public MenCatVM(ApplicationDbContext _context)
        {

            Context = _context;
            this.menuCategories = Context.MenuCategorie.ToList();

        }


        public IEnumerable<MenuCategory> menuCategories { get; set; }
        public string GetplaceName(int id) => Context.MenuCategorie.Where(p => p.PlaceID == id).Select(a => a.Place.PlaceName).FirstOrDefault();


    }
}
