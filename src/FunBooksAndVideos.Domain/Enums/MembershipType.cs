using System.ComponentModel;

namespace FunBooksAndVideos.Domain.Enums
{
    public enum MembershipType
    {
        [Description("Book Club Membership")]
        BookClubMembership = 1,

        [Description("Video Club Membership")]
        VideoClubMembership = 2,

        [Description("Premium Club Membership (Book & Video)")]
        PremiumClubMembership = 3 
    }
}
