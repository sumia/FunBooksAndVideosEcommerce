using FunBooksAndVideos.Application.Products.DTOs;
using FunBooksAndVideos.Domain.Entities.ShopItems;

namespace FunBooksAndVideos.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<Guid> Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
