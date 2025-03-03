using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.Application.PurchaseOrders.DTOs
{
    public class OrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }
    }
}
