using FunBooksAndVideos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        
        public async Task<T?> GetById(Guid id) => await _dbSet.FindAsync(id);

        public async Task<Guid> Create(T entity) {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            // Ensure the entity has an 'Id' property of type Guid
            var entityIdProperty = typeof(T).GetProperty("Id");
            if (entityIdProperty != null && entityIdProperty.PropertyType == typeof(Guid))
            {
                return (Guid)entityIdProperty.GetValue(entity)!;
            }

            throw new InvalidOperationException("Entity does not have a Guid Id property.");
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}
