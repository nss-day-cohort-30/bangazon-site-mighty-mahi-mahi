﻿// <auto-generated />
using System;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bangazon.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190605201718_userLikes")]
    partial class userLikes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bangazon.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("StreetAddress")
                        .IsRequired();

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "643365cc-8f2e-4021-8443-b9e0ba5ebf01",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Admina",
                            LastName = "Straytor",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEPmDlqCv0c2G73sWXvf+3axk6S4V42Mc92tUdoCI/SU+vIuKZRTsy7PGGMnSHILmWA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "255e1061-d423-4d95-9307-752812680612",
                            StreetAddress = "123 Infinity Way",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        },
                        new
                        {
                            Id = "cbe7cd27-3b41-4c02-81b6-112a315612df",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6e0ae90a-f7eb-4e78-9cc1-9529a64f5ec6",
                            Email = "brianbneal@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Brian",
                            LastName = "Neal",
                            LockoutEnabled = false,
                            NormalizedEmail = "BRIANBNEAL@GMAIL.COM",
                            NormalizedUserName = "BRIANBNEAL@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEMHyPzIAIIHd9FaLXV6QSHtiJoCT8z1P9kJFE4hSvivtNgcB1FY2nxcRnHtylWvuNA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6fd333e7-dc5e-4bd6-9cb7-3972234a712a",
                            StreetAddress = "1412 Phillips Street",
                            TwoFactorEnabled = false,
                            UserName = "brianbneal@gmail.com"
                        },
                        new
                        {
                            Id = "2c42da61-78b4-4c08-8508-b9b0bf261d91",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "932b1f97-68c0-46f5-acb2-ca458554c5a0",
                            Email = "bailey.connor.p@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Connor",
                            LastName = "Bailey",
                            LockoutEnabled = false,
                            NormalizedEmail = "bailey.connor.p@gmail.com",
                            NormalizedUserName = "BAILEY.CONNOR.P@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEO9f6syddB01ZtBr1rdLehk4QMq0udq6nQoBCVyOKxZIVmYLL04me8trt5jMTl5Jdg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3118b667-733f-4583-9d03-e4d018b6d6a4",
                            StreetAddress = "1619 Marshall Hollow ",
                            TwoFactorEnabled = false,
                            UserName = "bailey.connor.p@gmail.com"
                        },
                        new
                        {
                            Id = "a8581fd1-83e1-44fc-a0b2-5b84fd0c9ef2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c72ee53d-da5e-46bb-a385-a19fde8c6975",
                            Email = "niall@niall.com",
                            EmailConfirmed = true,
                            FirstName = "Niall",
                            LastName = "Fraser",
                            LockoutEnabled = false,
                            NormalizedEmail = "NIALL@NIALL.COM",
                            NormalizedUserName = "NIALL@NIALL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEC6GCEluXIvBSnHu1zSvPsMs8ORZtmhrYUS/30triFMYJP4ufqujoWlkNz89dcvISg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9562ba1a-be4e-4ce4-a9ae-db2d8db21afe",
                            StreetAddress = "123 Infinity Way",
                            TwoFactorEnabled = false,
                            UserName = "niall@niall.com"
                        },
                        new
                        {
                            Id = "4a209aac-299f-4b14-99fc-0915059f29af",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cea23f78-0682-44dc-86a9-b5f2de553297",
                            Email = "Jacob.Sanders@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Jacob",
                            LastName = "Sanders",
                            LockoutEnabled = false,
                            NormalizedEmail = "JACOB.SANDERS@GMAIL.COM",
                            NormalizedUserName = "JACOB.SANDERS@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEMHrVU0fdeNuX9O5V86yyWYUwzqqitzY7yHWfRQvYmu8fECxnjlZkJs7jT7gDP8/tw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f58d6637-3e17-408e-8449-5a24335c0d68",
                            StreetAddress = "89 Rainbow Road",
                            TwoFactorEnabled = false,
                            UserName = "Jacob.Sanders@gmail.com"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateCompleted");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("PaymentTypeId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("OrderId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");

                    b.HasData(
                        new
                        {
                            OrderProductId = 1,
                            OrderId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            OrderProductId = 2,
                            OrderId = 1,
                            ProductId = 2
                        });
                });

            modelBuilder.Entity("Bangazon.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("PaymentTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentType");

                    b.HasData(
                        new
                        {
                            PaymentTypeId = 1,
                            AccountNumber = "86753095551212",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "American Express",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b"
                        },
                        new
                        {
                            PaymentTypeId = 2,
                            AccountNumber = "4102948572991",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Discover",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b"
                        },
                        new
                        {
                            PaymentTypeId = 3,
                            AccountNumber = "8374958468590",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "VISA",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "4a209aac-299f-4b14-99fc-0915059f29af"
                        },
                        new
                        {
                            PaymentTypeId = 4,
                            AccountNumber = "44445445433563",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Visa",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "a8581fd1-83e1-44fc-a0b2-5b84fd0c9ef2"
                        },
                        new
                        {
                            PaymentTypeId = 5,
                            AccountNumber = "4982399357284112",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Diners Club Card",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "2c42da61-78b4-4c08-8508-b9b0bf261d91"
                        },
                        new
                        {
                            PaymentTypeId = 6,
                            AccountNumber = "8394572390128745",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "MasterCharge",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "2c42da61-78b4-4c08-8508-b9b0bf261d91"
                        },
                        new
                        {
                            PaymentTypeId = 7,
                            AccountNumber = "123456789101112",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Big Bucks Card",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "cbe7cd27-3b41-4c02-81b6-112a315612df"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ImagePath");

                    b.Property<double>("Price");

                    b.Property<int>("ProductTypeId");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<string>("UserId");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "It flies high",
                            Price = 2.9900000000000002,
                            ProductTypeId = 1,
                            Quantity = 100,
                            Title = "Kite",
                            UserId = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b"
                        },
                        new
                        {
                            ProductId = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "It rolls fast",
                            Price = 29.989999999999998,
                            ProductTypeId = 2,
                            Quantity = 5,
                            Title = "Wheelbarrow",
                            UserId = "ac48cd29-417d-44ee-b0ed-1a6ae5f8070b"
                        },
                        new
                        {
                            ProductId = 3,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "round and bouncy",
                            Price = 11.99,
                            ProductTypeId = 1,
                            Quantity = 10,
                            Title = "Ball",
                            UserId = "a8581fd1-83e1-44fc-a0b2-5b84fd0c9ef2"
                        },
                        new
                        {
                            ProductId = 4,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "You will never see a price point like this again",
                            Price = 5.0,
                            ProductTypeId = 1,
                            Quantity = 6,
                            Title = "Bugatti",
                            UserId = "4a209aac-299f-4b14-99fc-0915059f29af"
                        },
                        new
                        {
                            ProductId = 5,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Can shoot t-shirts (or hot dogs) up to 600 meters when used at max air pressure",
                            Price = 99.989999999999995,
                            ProductTypeId = 1,
                            Quantity = 8,
                            Title = "T-Shirt Cannon",
                            UserId = "2c42da61-78b4-4c08-8508-b9b0bf261d91"
                        },
                        new
                        {
                            ProductId = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "it's a stick",
                            Price = 5.9900000000000002,
                            ProductTypeId = 1,
                            Quantity = 200,
                            Title = "back scratcher",
                            UserId = "cbe7cd27-3b41-4c02-81b6-112a315612df"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");

                    b.HasData(
                        new
                        {
                            ProductTypeId = 1,
                            Label = "Sporting Goods"
                        },
                        new
                        {
                            ProductTypeId = 2,
                            Label = "Appliances"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.UserProductLike", b =>
                {
                    b.Property<int>("UserProductLikeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsLiked");

                    b.Property<int>("ProductId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("UserProductLikeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProductLike");
                });

            modelBuilder.Entity("Bangazon.Models.UserProductRating", b =>
                {
                    b.Property<int>("UserProductRatingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("Rating");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("UserProductRatingId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProductRating");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Bangazon.Models.Order", b =>
                {
                    b.HasOne("Bangazon.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeId");

                    b.HasOne("Bangazon.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.OrderProduct", b =>
                {
                    b.HasOne("Bangazon.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bangazon.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Bangazon.Models.PaymentType", b =>
                {
                    b.HasOne("Bangazon.Models.ApplicationUser", "User")
                        .WithMany("PaymentTypes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.HasOne("Bangazon.Models.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ApplicationUser", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Bangazon.Models.UserProductLike", b =>
                {
                    b.HasOne("Bangazon.Models.Product", "Product")
                        .WithMany("Likes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ApplicationUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.UserProductRating", b =>
                {
                    b.HasOne("Bangazon.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ApplicationUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Bangazon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Bangazon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Bangazon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
