using Asp.Versioning;
using FunBooksAndVideos.API.Models;
using FunBooksAndVideos.Application.Features.Products.Exceptions;
using FunBooksAndVideos.Application.PurchaseOrders.DTOs;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion(1.0)]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        /// <summary>
        /// Processes an order for a customer.
        /// </summary>
        /// <param name="orderDto">Order details</param>
        /// <returns>OrderId</returns>
        [HttpPost("ProcessOrder")]
        [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessOrder([FromBody] OrderRequestDto orderDto)
        {
            _logger.LogInformation("Received request to process order for customer {CustomerId}", orderDto.CustomerId);
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Invalid request model: {Errors}", string.Join(", ", errors));
                return BadRequest(new ErrorResponse("Validation failed", errors));
            }

            try 
            {
                var purchaseOrder = new PurchaseOrder
                {
                    CustomerId = orderDto.CustomerId,
                    TotalAmount = orderDto.TotalPrice,
                    Items = orderDto.Items.Select(p => new PurchaseOrderItem
                    {
                        ProductId = p.ProductId
                    }).ToList()
                };
                _logger.LogInformation("Processing order for CustomerId: {CustomerId} with {ItemCount} items", orderDto.CustomerId, purchaseOrder.Items.Count);
                Guid orderId = await _orderService.ProcessOrder(purchaseOrder);


                _logger.LogInformation("Order {OrderId} processed successfully for CustomerId {CustomerId}", purchaseOrder.Id, purchaseOrder.CustomerId);
                return Ok(new OrderResponseDto{ OrderId = purchaseOrder.Id, Message = "Order processed successfully." });
            }
            catch (Exception ex) when 
                (ex is ArgumentException || ex is ProductNotFoundException || ex is CustomerNotFoundException)
            {
                _logger.LogError($"An exception occurred while processing order: {ex.StackTrace}");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }
    }
}
