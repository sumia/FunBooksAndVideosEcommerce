using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Domain.Entities.Memberships
{
    public class Membership
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public MembershipType MembershipType { get; set; }
        public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.Active;


        // Navigation properties
        public Customer Customer { get; set; }
        public Product Product { get; set; }


        public override string ToString()
        {
            return MembershipType.GetDescription();
        }
    }
}
