using FunBooksAndVideos.Application.Memberships.Interfaces;
using FunBooksAndVideos.Application.Memberships.Services;
using FunBooksAndVideos.Application.Products.Interfaces;
using FunBooksAndVideos.Application.Products.Services;
using FunBooksAndVideos.Application.PurchaseOrders.Factories;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Application.PurchaseOrders.Rules;
using FunBooksAndVideos.Application.PurchaseOrders.Services;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using FunBooksAndVideos.Infrastructure.Persistence.Respositories;
using Microsoft.Win32;
using System.Reflection;

namespace FunBooksAndVideos.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IShippingService, ShippingService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
