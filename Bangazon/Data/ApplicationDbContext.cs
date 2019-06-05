using System;
using System.Collections.Generic;
using System.Text;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<UserProductRating> UserProductRating { get; set; }
        public DbSet<UserProductLike> UserProductLike { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Order> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Order> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Order)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Product> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<Product> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Product)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            ApplicationUser user = new ApplicationUser {
                FirstName = "Admina",
                LastName = "Straytor",
                StreetAddress = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid ().ToString ("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser> ();
            user.PasswordHash = passwordHash.HashPassword (user, "Admin8*");
            modelBuilder.Entity<ApplicationUser> ().HasData (user);

            ApplicationUser brian = new ApplicationUser
            {
                FirstName = "Brian",
                LastName = "Neal",
                StreetAddress = "1412 Phillips Street",
                UserName = "brianbneal@gmail.com",
                NormalizedUserName = "BRIANBNEAL@GMAIL.COM",
                Email = "brianbneal@gmail.com",
                NormalizedEmail = "BRIANBNEAL@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash2 = new PasswordHasher<ApplicationUser>();
            brian.PasswordHash = passwordHash2.HashPassword(brian, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(brian);

            ApplicationUser connor = new ApplicationUser
            {
                FirstName = "Connor",
                LastName = "Bailey",
                StreetAddress = "1619 Marshall Hollow ",
                UserName = "bailey.connor.p@gmail.com",
                NormalizedUserName = "BAILEY.CONNOR.P@GMAIL.COM",
                Email = "bailey.connor.p@gmail.com",
                NormalizedEmail = "bailey.connor.p@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash3 = new PasswordHasher<ApplicationUser>();
            connor.PasswordHash = passwordHash3.HashPassword(connor, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(connor);

            ApplicationUser niall = new ApplicationUser
            {
                FirstName = "Niall",
                LastName = "Fraser",
                StreetAddress = "123 Infinity Way",
                UserName = "niall@niall.com",
                NormalizedUserName = "NIALL@NIALL.COM",
                Email = "niall@niall.com",
                NormalizedEmail = "NIALL@NIALL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash4 = new PasswordHasher<ApplicationUser>();
            niall.PasswordHash = passwordHash4.HashPassword(niall, "Niall1*");
            modelBuilder.Entity<ApplicationUser>().HasData(niall);

            ApplicationUser jacob = new ApplicationUser
            {
                FirstName = "Jacob",
                LastName = "Sanders",
                StreetAddress = "89 Rainbow Road",
                UserName = "Jacob.Sanders@gmail.com",
                NormalizedUserName = "JACOB.SANDERS@GMAIL.COM",
                Email = "Jacob.Sanders@gmail.com",
                NormalizedEmail = "JACOB.SANDERS@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash5 = new PasswordHasher<ApplicationUser>();
            jacob.PasswordHash = passwordHash5.HashPassword(jacob, "Thebeesknees09*");
            modelBuilder.Entity<ApplicationUser>().HasData(jacob);




            ////////////////////////////////////////////
            modelBuilder.Entity<PaymentType> ().HasData (
                new PaymentType () {
                    PaymentTypeId = 1,
                        UserId = user.Id,
                        Description = "American Express",
                        AccountNumber = "86753095551212"
                },
                new PaymentType () {
                    PaymentTypeId = 2,
                        UserId = user.Id,
                        Description = "Discover",
                        AccountNumber = "4102948572991"
                },
                new PaymentType()
                {
                    PaymentTypeId = 3,
                    UserId = jacob.Id,
                    Description = "VISA",
                    AccountNumber = "8374958468590"
                },
                new PaymentType()
                {
                    PaymentTypeId = 4,
                    UserId = niall.Id,
                    Description = "Visa",
                    AccountNumber = "44445445433563"
                },
                new PaymentType()
                {
                    PaymentTypeId = 5,
                    UserId = connor.Id,
                    Description = "Diners Club Card",
                    AccountNumber = "4982399357284112"
                },
                new PaymentType()
                {
                    PaymentTypeId = 6,
                    UserId = connor.Id,
                    Description = "MasterCharge",
                    AccountNumber = "8394572390128745"
                },
                new PaymentType()
                {
                    PaymentTypeId = 7,
                    UserId = brian.Id,
                    Description = "Big Bucks Card",
                    AccountNumber = "123456789101112"
                }
            );

            modelBuilder.Entity<ProductType> ().HasData (
                new ProductType () {
                    ProductTypeId = 1,
                        Label = "Sporting Goods"
                },
                new ProductType () {
                    ProductTypeId = 2,
                        Label = "Appliances"
                }
            );

            modelBuilder.Entity<Product> ().HasData (
                new Product () {
                    ProductId = 1,
                        ProductTypeId = 1,
                        UserId = user.Id,
                        Description = "It flies high",
                        Title = "Kite",
                        Quantity = 100,
                        Price = 2.99
                },
                new Product () {
                    ProductId = 2,
                        ProductTypeId = 2,
                        UserId = user.Id,
                        Description = "It rolls fast",
                        Title = "Wheelbarrow",
                        Quantity = 5,
                        Price = 29.99
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTypeId = 1,
                    UserId = niall.Id,
                    Description = "round and bouncy",
                    Title = "Ball",
                    Quantity = 10,
                    Price = 11.99
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTypeId = 1,
                    UserId = jacob.Id,
                    Description = "You will never see a price point like this again",
                    Title = "Bugatti",
                    Quantity = 6,
                    Price = 5.00
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 1,
                    UserId = connor.Id,
                    Description = "Can shoot t-shirts (or hot dogs) up to 600 meters when used at max air pressure",
                    Title = "T-Shirt Cannon",
                    Quantity = 8,
                    Price = 99.99
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTypeId = 1,
                    UserId = brian.Id,
                    Description = "it's a stick",
                    Title = "back scratcher",
                    Quantity = 200,
                    Price = 5.99
                }
            );

            modelBuilder.Entity<Order> ().HasData (
                new Order () {
                    OrderId = 1,
                    UserId = user.Id,
                    PaymentTypeId = null
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct () {
                    OrderProductId = 1,
                    OrderId = 1,
                    ProductId = 1
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct () {
                    OrderProductId = 2,
                    OrderId = 1,
                    ProductId = 2
                }
            );

        }
    }
}