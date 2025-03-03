using FunBooksAndVideos.Application.Memberships.DTOs;
using FunBooksAndVideos.Application.Memberships.Interfaces;
using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;

namespace FunBooksAndVideos.Application.Memberships.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MembershipResponseDto> GetMembershipsByCustomerId(Guid customerId)
        {
            var memberships=  await _unitOfWork.Memberships.GetByCustomerId(customerId);
            var membershipDto = new MembershipResponseDto
            {
                CustomerId = customerId,
                Memberships = memberships.Select(m => m.MembershipType.GetDescription()).ToList()
            };

            return membershipDto;
        }

        public async Task<bool> ActivateMembership(Membership membership)
        {
            try { 
                await _unitOfWork.Memberships.Create(membership);
                await _unitOfWork.SaveChangesAsync();
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }
    }
}
