using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using FunBooksAndVideos.Infrastructure.Repositories;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IGenericRepository<Customer> Customers { get; private set; }
        public IGenericRepository<PurchaseOrder> PurchaseOrders { get; private set; }
        public IGenericRepository<PurchaseOrderItem> PurchaseOrderItems { get; private set; }
        public IGenericRepository<ShippingSlip> ShippingSlips { get; private set; }
        public IMembershipRepository Memberships { get; private set; }
        public IProductRepository Products { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Customers = new GenericRepository<Customer>(dbContext);
            PurchaseOrders = new GenericRepository<PurchaseOrder>(dbContext);
            PurchaseOrderItems = new GenericRepository<PurchaseOrderItem>(dbContext);
            ShippingSlips = new GenericRepository<ShippingSlip>(dbContext);
            Memberships = new MembershipRepository(_dbContext);
            Products = new ProductRepository(dbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
