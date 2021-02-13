using Microsoft.CodeAnalysis.CSharp.Syntax;
using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.ViewModel
{
    public class MenuVM
    {
        ApplicationDbContext Context;
        public MenuVM(ApplicationDbContext _context) {

            Context = _context;
            this.menu = Context.Menu.ToList();
            this.places = Context.Place.ToList();

        }


        public IEnumerable<Menu> menu{ get; set; }
        public IEnumerable<Place> places { get; set; }
        public string GetCatgory(int id) => Context.Menu.Where(p => p.MenuCategoryID == id).Select(a => a.MenuCategory.MenuCategoryType).FirstOrDefault();


    }
}
