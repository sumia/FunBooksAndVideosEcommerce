using FunBooksAndVideos.Domain.Entities;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.API.Extensions
{
    public static class DatabaseExtensions 
    { 
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("ShawbrookDB")); // Use an in-memory database
        }

        public static void ApplyDatabaseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // No need for migrations when using an in-memory database
            dbContext.Database.EnsureCreated(); // Ensures the database is created in memory
            SeedData(dbContext);
        }

        private static void SeedData(AppDbContext dbContext)
        {
            SeedProducts(dbContext);
            SeedCustomers(dbContext);
        }

        private static void SeedCustomers(AppDbContext dbContext)
        {
            if(!dbContext.Customers.Any())
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Ben",
                    LastName = "Smith",
                    Email = "ben@gmail.com",
                    Phone = "123456789"
                };

                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }
        }

        private static void SeedProducts(AppDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "The Girl on the train",
                        Price = 20,
                        ProductCategory = ProductCategory.Book,
                        ProductType = ProductType.Physical
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "The Girl on the train - Part 2",
                        Price = 25,
                        ProductCategory = ProductCategory.Book,
                        ProductType = ProductType.Physical
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Comprehensive First Aid Training",
                        Price = 15,
                        ProductCategory = ProductCategory.Video,
                        ProductType = ProductType.Digital
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Book Club Membership",
                        Price = 3,
                        ProductCategory = ProductCategory.BookClub,
                        ProductType = ProductType.Membership
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Video Club Membership",
                        Price = 5,
                        ProductCategory = ProductCategory.VideoClub,
                        ProductType = ProductType.Membership
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Premium Club Membership",
                        Price = 7.50M,
                        ProductCategory = ProductCategory.PremiumClub,
                        ProductType = ProductType.Membership
                    },
                };

                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }
    }
}
