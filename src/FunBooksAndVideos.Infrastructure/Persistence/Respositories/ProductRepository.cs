using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetByIds(ICollection<Guid> productIds)
        {
            return await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
        }
    }
}
