using FunBooksAndVideos.Domain.Entities.Order;

namespace FunBooksAndVideos.Infrastructure.Persistence.Respositories
{
    public class OrderRepository : GenericRepository<PurchaseOrder>
    {

        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
