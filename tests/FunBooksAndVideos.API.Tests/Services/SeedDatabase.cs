using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.API.Tests.Services
{
    internal class SeedDatabase
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            await SeedProducts(context);
            await SeedCustomers(context);
        }


        private static async Task SeedProducts(AppDbContext dbContext)
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

        private static async Task SeedCustomers(AppDbContext dbContext)
        {
            if (!dbContext.Customers.Any())
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Test firstname",
                    LastName = "test lastname",
                    Email = "test@gmail.com",
                    Phone = "123456789"
                };

                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }
        }
    }
}
