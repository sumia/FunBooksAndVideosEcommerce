using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.Application.PurchaseOrders.DTOs
{
    public class OrderRequestDto
    {

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public List<OrderItemDto> Items { get; set; }
    }
}
