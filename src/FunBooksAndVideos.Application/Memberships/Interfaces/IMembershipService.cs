using FunBooksAndVideos.Application.Memberships.DTOs;
using FunBooksAndVideos.Domain.Entities.Memberships;

namespace FunBooksAndVideos.Application.Memberships.Interfaces
{
    public interface IMembershipService
    {
        Task<MembershipResponseDto> GetMembershipsByCustomerId(Guid customerId);
        Task<bool> ActivateMembership(Membership membership);
    }
}
