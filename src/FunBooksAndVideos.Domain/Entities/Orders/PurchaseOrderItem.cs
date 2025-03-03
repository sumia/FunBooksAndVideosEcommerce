using FunBooksAndVideos.Domain.Entities.ShopItems;

namespace FunBooksAndVideos.Domain.Entities.Order
{
    public sealed class PurchaseOrderItem
    {
        public int Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }

        // Navigation Property
        public Product Product { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
