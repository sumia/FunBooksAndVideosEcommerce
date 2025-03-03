using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderLines { get; set; }
        public DbSet<ShippingSlip> ShippingSlips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderItemConfiguration());
        }
    }
}
