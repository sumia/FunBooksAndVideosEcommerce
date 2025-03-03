using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Infrastructure.Repositories;

namespace FunBooksAndVideos.Infrastructure.Persistence.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByIds(ICollection<Guid> productIds);
    }
}
