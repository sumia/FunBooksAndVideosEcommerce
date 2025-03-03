using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Infrastructure.Repositories;

namespace FunBooksAndVideos.Infrastructure.Persistence.Interfaces
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<ICollection<Membership>> GetByCustomerId(Guid customerId);
    }
}
