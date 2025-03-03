using FunBooksAndVideos.Domain.Entities.Order;

namespace FunBooksAndVideos.Application.PurchaseOrders.Interfaces
{
    public interface IOrderRule
    {
        bool IsApplicable(PurchaseOrder order);
        Task Apply(PurchaseOrder order);
    }
}
