using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Rookie.DataAccessor.Data
{
    public class EcomDbContext : IdentityDbContext<AppUser,AppRole, Guid>
    {
        public EcomDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId});
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin").HasKey(x => x.UserId);


            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            //base.OnModelCreating(builder);
            //Seed
            var roleId = new Guid("44AEDB87-3A2E-4EC4-AA46-85E3F332A796");
            var adminId = new Guid("C8DCB1FD-A46C-4068-B700-54ADC575660C");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "huycnh@gmail.com",
                NormalizedEmail = "huycnh@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "@bcd1234"),
                SecurityStamp = string.Empty,
                FirstName = "Huy",
                LastName = "Chau",
                Dob = new DateTime(1999, 09, 13),
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Address> Addresses { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get; }
        public DbSet<Rating> Ratings { set; get; }
        public DbSet<ShipDetail> ShipDetails { set; get; }
    }
}
