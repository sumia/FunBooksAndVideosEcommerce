using FunBooksAndVideos.Application.Memberships.Interfaces;
using FunBooksAndVideos.Application.Memberships.Services;
using FunBooksAndVideos.Application.PurchaseOrders.Factories;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Application.PurchaseOrders.Rules;
using FunBooksAndVideos.Application.PurchaseOrders.Services;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Entities.Shipping;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using Moq;
using System.Collections.ObjectModel;

namespace FunBooksAndVideos.Application.Tests.Services
{
    public class OrderProcessorTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly OrderService _orderService;

        public OrderProcessorTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            OrderRuleFactory orderRuleFactory = GetMockedOrderRuleFactory();
            _orderService = new OrderService(_unitOfWorkMock.Object, orderRuleFactory);
        }

       
        [Fact]
        public async Task ProcessOrder_ShouldActivateMembership_WhenOrderContainsMembershipProduct()
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test firstname",
                LastName = "test lastname",
                Email = "test@gmail.com",
                Phone = "123456789"
            };

            var products = new Collection<Product> { 
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Book Club Membership",
                    Price = 3,
                    ProductCategory = ProductCategory.BookClub,
                    ProductType = ProductType.Membership
                }
            };

            var purchaseOrder = new PurchaseOrder
            {
                CustomerId = customer.Id,
                Customer = customer,
                TotalAmount = 20,
                Items = new List<PurchaseOrderItem>
                {
                    new PurchaseOrderItem
                    {
                        ProductId = products.ElementAt(0).Id,
                        Product = products.ElementAt(0),
                    }
                }
            };


            _unitOfWorkMock
                .Setup(u => u.Customers.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _unitOfWorkMock
                .Setup(u => u.Products.GetByIds(It.IsAny<ICollection<Guid>>()))
                .ReturnsAsync(products);

            _unitOfWorkMock
                 .Setup(u => u.PurchaseOrders.Create(purchaseOrder))
                 .ReturnsAsync(Guid.NewGuid());


            _unitOfWorkMock
                .Setup(u => u.Memberships.Create(It.IsAny<Membership>()))
                .ReturnsAsync(Guid.NewGuid());


            // Act
            await _orderService.ProcessOrder(purchaseOrder);

            // Assert
            _unitOfWorkMock.Verify(u =>
               u.Memberships.Create(It.IsAny<Membership>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ProcessOrder_ShouldGenerateShippingSlip_WhenOrderContainsMembershipProduct()
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test firstname",
                LastName = "test lastname",
                Email = "test@gmail.com",
                Phone = "123456789"
            };

            var products = new Collection<Product> {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "The Girl on the train",
                    Price = 3,
                    ProductCategory = ProductCategory.Book,
                    ProductType = ProductType.Physical
                }
            };

            var purchaseOrder = new PurchaseOrder
            {
                CustomerId = customer.Id,
                Customer = customer,
                TotalAmount = 20,
                Items = new List<PurchaseOrderItem>
                {
                    new PurchaseOrderItem
                    {
                        ProductId = products.ElementAt(0).Id,
                        Product = products.ElementAt(0),
                    }
                }
            };


            _unitOfWorkMock
                .Setup(u => u.Customers.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            _unitOfWorkMock
                .Setup(u => u.Products.GetByIds(It.IsAny<ICollection<Guid>>()))
                .ReturnsAsync(products);

            _unitOfWorkMock
                 .Setup(u => u.PurchaseOrders.Create(purchaseOrder))
                 .ReturnsAsync(Guid.NewGuid());


            _unitOfWorkMock
                .Setup(u => u.ShippingSlips.Create(It.IsAny<ShippingSlip>()))
                .ReturnsAsync(Guid.NewGuid());


            // Act
            await _orderService.ProcessOrder(purchaseOrder);

            // Assert
            _unitOfWorkMock.Verify(u =>
               u.ShippingSlips.Create(It.IsAny<ShippingSlip>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.AtLeastOnce);
        }

        private OrderRuleFactory GetMockedOrderRuleFactory()
        {
            var membershipService = new MembershipService(_unitOfWorkMock.Object);
            var shippingService = new ShippingService(_unitOfWorkMock.Object);

            var membershipRuleMock = new MembershipActivationRule(membershipService);
            var shippingRuleMock = new ShippingSlipRule(shippingService);


            var membershipRule = new MembershipActivationRule(membershipService);
            var shippingRule = new ShippingSlipRule(shippingService);

            var rules = new List<IOrderRule> { membershipRule, shippingRule };

            var orderRuleFactory = new OrderRuleFactory(rules);
            return orderRuleFactory;
        }
    }
}
