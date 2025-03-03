using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.Application.PurchaseOrders.DTOs
{
    public class OrderResponseDto
    {
        public Guid? OrderId { get; set; }
        public string Message { get; set; }

    }
}
