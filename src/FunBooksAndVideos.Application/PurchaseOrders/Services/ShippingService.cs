using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using System.Text;

namespace FunBooksAndVideos.Application.PurchaseOrders.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShippingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> GenerateShippingSlip(PurchaseOrder order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================");
            sb.Append("        SHIPPING SLIP           ");
            sb.Append("================================");
            sb.Append($"Order ID: {order.Id}");
            sb.Append($"Customer: {order.CustomerId}");

            var shippingSlip = new ShippingSlip
            {
                PurchaseOrderId = order.Id,
                Content = sb.ToString()
            };

            try
            {
                await _unitOfWork.ShippingSlips.Create(shippingSlip);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
