using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly AppDbContext _dbContext;

        public MembershipRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Membership>> GetByCustomerId(Guid customerId)
        {
            return await _dbContext.Memberships.Where(m =>
                m.CustomerId == customerId).ToListAsync();
        }
    }
}