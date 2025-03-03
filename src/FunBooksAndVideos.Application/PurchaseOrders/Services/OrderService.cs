using FunBooksAndVideos.Application.Features.Products.Exceptions;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Exceptions;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;

namespace FunBooksAndVideos.Application.PurchaseOrders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IOrderRule> _rules;

        public OrderService(IUnitOfWork unitOfWork, IOrderRuleFactory ruleFactory)
        {
            _unitOfWork = unitOfWork;
            _rules = ruleFactory.GetRules();
        }

        public async Task<Guid> ProcessOrder(PurchaseOrder order)
        {
            // validate
            if (order == null || !order.Items.Any())
            {
                throw new ArgumentException("Order cannot be null or empty.");
            }

            var productIds = order.Items.Select(item => item.ProductId).ToList();
            var existingProducts = (await _unitOfWork.Products.GetByIds(productIds)).ToHashSet();

            var missingProductIds = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
            if (missingProductIds.Any())
            {
                throw new ProductNotFoundException($"Products with the following IDs were not found:\n {string.Join("\n", missingProductIds)}");

            }

            var customer = await _unitOfWork.Customers.GetById(order.CustomerId);
            if (customer == null) throw new CustomerNotFoundException(order.CustomerId);


            // save order
            await _unitOfWork.PurchaseOrders.Create(order);
            await _unitOfWork.SaveChangesAsync();


            // apply rules
            var applicableRules = _rules.Where(rule => rule.IsApplicable(order)).ToList();
            await Task.WhenAll(applicableRules.Select(rule => rule.Apply(order)));

            return order.Id;
        }
    }
}
