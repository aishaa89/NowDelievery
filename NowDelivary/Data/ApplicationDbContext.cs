using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NowDelivary.Models;

namespace NowDelivary.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Area> Area { get; set; }
        public DbSet<CompanyIncome> CompanyIncome { get; set; }
        public DbSet<CustomerAddress> CustomerAddresse { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuCategory> MenuCategorie { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderInformation> OrderInformation { get; set; }
        public DbSet<OrderMenuItems> OrderMenuItems { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<PlaceCategory> PlaceCategory { get; set; }
        public DbSet<DelivaryCost> DelivaryCost { get; set; }
    }
}
