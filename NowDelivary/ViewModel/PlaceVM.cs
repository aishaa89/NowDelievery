using NowDelivary.Data;
using NowDelivary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowDelivary.View_Model
{
    public class PlaceVM
    {
        ApplicationDbContext Context;
        public PlaceVM(ApplicationDbContext _context)
        {
            Context = _context;
            this.Places = Context.Place.ToList();
        }


        public IEnumerable<Place> FilterPlacesByAreaAndCategory(int areaID , int placeCategoryID) => Context.Place.Where(p => p.AreaID == areaID && p.PlaceCategoryID == placeCategoryID).ToList();


        public int menuItemQuantity { get; set; }

        public string getAreaNameByPlace(int placeID) => Context.Place.Where(p => p.ID == placeID).Select(a => a.Area.AreaName).FirstOrDefault();

        public IEnumerable<Place> GetAllPharmacies(int areaID) => Context.Place.Where(p => p.PlaceCategory.PlaceCategoryName == "Pharmacy" && p.AreaID==areaID).ToList();
        public IEnumerable<Place> GetAllSupermarket(int areaID) => Context.Place.Where(p => p.PlaceCategory.PlaceCategoryName == "SuperMarket" && p.AreaID == areaID).ToList();

        public IEnumerable<Place> Places { get; set; }
        public string GetAreaName(int areaId) => Context.Place.Where(p => p.AreaID == areaId).Select(a => a.Area.AreaName).FirstOrDefault();
        public string GetPlaceCategoryName(int id) => Context.Place.Where(p => p.PlaceCategoryID == id).Select(c => c.PlaceCategory.PlaceCategoryName).FirstOrDefault();

        public IEnumerable<MenuCategory> GetSupermarketMenuCategory(int supermarketID) => Context.MenuCategorie.Where(m => m.PlaceID == supermarketID).ToList();
        public IEnumerable<Menu> GetMenu(int menuCategoryID) => Context.Menu.Where(m => m.MenuCategoryID == menuCategoryID).ToList();

        public Place GetPlaceByMenuCategory(int menuCategoryID) => Context.MenuCategorie.Find(menuCategoryID).Place;

    }
}
