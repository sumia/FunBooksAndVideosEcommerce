namespace FunBooksAndVideos.Domain.Entities.Shipping
{
    public class ShippingSlip
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
