using FluentAssertions;
using FunBooksAndVideos.Application.PurchaseOrders.Services;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using Moq;

namespace FunBooksAndVideos.Application.Tests.Services
{
    public class ShippingServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ShippingService _shippingService;

        public ShippingServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _shippingService = new ShippingService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GenerateShippingSlip_ShouldGenerateShippingSlip_WhenOrderContainsPhysicalProduct()
        {
            var purchaseOrder = new PurchaseOrder
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<PurchaseOrderItem> {
                    new PurchaseOrderItem { ProductId = Guid.NewGuid() }
                }
            };

            _unitOfWorkMock
                .Setup(u => u.ShippingSlips.Create(It.IsAny<ShippingSlip>()))
                .ReturnsAsync(Guid.NewGuid());
            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var generated = await _shippingService.GenerateShippingSlip(purchaseOrder);
            generated.Should().Be(true);
        }
    }
}
