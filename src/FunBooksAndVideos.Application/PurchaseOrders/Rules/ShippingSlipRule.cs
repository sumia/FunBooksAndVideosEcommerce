using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.PurchaseOrders.Rules
{
    public class ShippingSlipRule : IOrderRule
    {
        private readonly IShippingService _shippingService;

        public ShippingSlipRule(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public bool IsApplicable(PurchaseOrder order)
        {
            return order.Items.Any(item => item.Product.ProductType == ProductType.Physical);
        }

        public async Task Apply(PurchaseOrder order)
        {
            await _shippingService.GenerateShippingSlip(order);
        }
    }
}
