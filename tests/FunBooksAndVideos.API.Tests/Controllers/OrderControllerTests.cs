using FluentAssertions;
using FunBooksAndVideos.API.Tests.Services;
using FunBooksAndVideos.Application.PurchaseOrders.DTOs;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Domain.Enums;
using System.Net;
using System.Net.Http.Json;

namespace FunBooksAndVideos.API.Tests.Controllers
{
    [Collection("IntegrationTestCollection")]
    public class OrderControllerTests
    {
        private const string ApiUrl = "/api/v1";
        private readonly HttpClient _client;

        public OrderControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ProcessOrder_ShouldReturnBadRequest_WhenOrderIsNull()
        {
            // Arrange
            OrderRequestDto purchaseOrderDto = null;

            // Act
            var response = await _client.PostAsJsonAsync($"{ApiUrl}/orders/ProcessOrder", purchaseOrderDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ProcessOrder_ShouldReturnBadRequest_WhenCustomerIsMissingInPayload()
        {
            // Arrange
            var product = await GetMembershipProduct(ProductCategory.Book);
            OrderRequestDto purchaseOrderDto = new OrderRequestDto
            {
                TotalPrice = 10,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = product.Id }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync($"{ApiUrl}/orders/ProcessOrder", purchaseOrderDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ProcessOrder_ShouldReturnBadRequest_WhenProductDoesNotExist()
        {
            // Arrange
            OrderRequestDto purchaseOrderDto = new OrderRequestDto
            {
                TotalPrice = 10,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = Guid.NewGuid() }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync($"{ApiUrl}/orders/ProcessOrder", purchaseOrderDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task ProcessOrder_ShouldReturnOk_WhenOrderIsValid()
        {
            // Arrange
            var customer = await GetCustomer();
            var product = await GetMembershipProduct(ProductCategory.Book);
            OrderRequestDto purchaseOrderDto = new OrderRequestDto
            {
                CustomerId = customer.Id,
                TotalPrice = 10,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = product.Id }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync($"{ApiUrl}/orders/ProcessOrder", purchaseOrderDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var dto = await response.Content.ReadFromJsonAsync<OrderResponseDto>();
            dto.OrderId.Should().NotBeNull();
        }

        private async Task<Product> GetMembershipProduct(ProductCategory productCategory)
        {
            var response = await _client.GetAsync($"{ApiUrl}/products");
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            var clubProduct = products.Where(p => p.ProductCategory == productCategory)
                .FirstOrDefault();

            return clubProduct;
        }

        private async Task<Customer> GetCustomer()
        {
            var response = await _client.GetAsync($"{ApiUrl}/customers");
            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();

            return customers.FirstOrDefault();
        }
    }
}