using FunBooksAndVideos.Domain.Entities.Order;

namespace FunBooksAndVideos.Application.PurchaseOrders.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> ProcessOrder(PurchaseOrder order);
    }
}
