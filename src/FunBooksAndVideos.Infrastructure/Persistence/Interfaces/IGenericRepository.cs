using FunBooksAndVideos.Domain.Entities.ShopItems;

namespace FunBooksAndVideos.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<Guid> Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
