using FunBooksAndVideos.Application.Products.Interfaces;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;

namespace FunBooksAndVideos.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAll();
            return products;
        }

        public async Task<Product?> GetById(Guid id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            return product;
        }

        public async Task<Guid> Create(Product product)
        {
            var productId = await _unitOfWork.Products.Create(product);
            await _unitOfWork.SaveChangesAsync();

            return productId;
        }

        public async Task Update(Product product)
        {
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
