using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
