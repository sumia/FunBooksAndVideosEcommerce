using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Domain.Entities.Order;

namespace FunBooksAndVideos.Domain.Entities.Customers
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public Membership Membership { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; private set; } = new List<PurchaseOrder>();

    }
}
