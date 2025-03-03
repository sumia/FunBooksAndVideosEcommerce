using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Infrastructure.Repositories;

namespace FunBooksAndVideos.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<PurchaseOrder> PurchaseOrders { get; }
        IGenericRepository<PurchaseOrderItem> PurchaseOrderItems { get; }
        IGenericRepository<ShippingSlip> ShippingSlips { get; }
        IProductRepository Products { get; }
        IMembershipRepository Memberships { get; }

        Task<int> SaveChangesAsync();
    }
}
