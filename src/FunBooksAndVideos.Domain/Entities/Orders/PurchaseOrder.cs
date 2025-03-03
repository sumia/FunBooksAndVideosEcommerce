using FunBooksAndVideos.Domain.Entities.Customers;

namespace FunBooksAndVideos.Domain.Entities.Order
{
    public sealed class PurchaseOrder
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();

        // Navigation Property
        public Customer Customer { get; set; }
    }
}
