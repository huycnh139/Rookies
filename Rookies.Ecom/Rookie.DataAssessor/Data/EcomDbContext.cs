using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Data
{
    public class EcomDbContext :DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options ) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Address> Addresses { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get; }
        public DbSet<Rating> Ratings { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<ShipDetail> ShipDetails { set; get; }

    }
}
