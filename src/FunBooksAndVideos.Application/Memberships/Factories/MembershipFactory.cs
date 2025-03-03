using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Memberships.Factories
{
    public static class MembershipFactory
    {
        private static Dictionary<ProductCategory, MembershipType> factory = new Dictionary<ProductCategory, MembershipType>
        {
            { ProductCategory.BookClub, MembershipType.BookClubMembership },
            { ProductCategory.VideoClub, MembershipType.VideoClubMembership },
            { ProductCategory.PremiumClub, MembershipType.PremiumClubMembership }
        };

        public static Membership CreateMembershipObject(Guid customerId, Guid productId, ProductCategory productCategory)
        {
            var membershipType = factory[productCategory];
            Membership membership = new Membership
            {
                CustomerId = customerId,
                ProductId = productId,
                MembershipType = membershipType
            };

            return membership;
        }

    }
}
