using FunBooksAndVideos.Application.Memberships.Factories;
using FunBooksAndVideos.Application.Memberships.Interfaces;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;
using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.PurchaseOrders.Rules
{
    public class MembershipActivationRule : IOrderRule
    {
        private readonly IMembershipService _membershipService;

        public MembershipActivationRule(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public bool IsApplicable(PurchaseOrder order)
        {
            return order.Items.Any(itemLine => itemLine.Product.ProductType == ProductType.Membership);
        }

        public async Task Apply(PurchaseOrder order)
        {
            var membershipItems = order.Items.Where(itemLine => itemLine.Product.ProductType == ProductType.Membership).ToList();

            foreach(var item in membershipItems)
            {
                var membership = MembershipFactory.CreateMembershipObject(order.Customer.Id,
                        item.ProductId, item.Product.ProductCategory);
                var success = await _membershipService.ActivateMembership(membership);
            }
        }
    }
}
