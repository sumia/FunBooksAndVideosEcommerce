using FunBooksAndVideos.Domain.Entities.Order;
using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Domain.Entities.ShopItems
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductType ProductType { get; set; }

        // Navigation Property
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();


        public Product() { }

        public Product(Guid id, string name, decimal price, ProductCategory productCategory, ProductType productType)
        {
            Id = id;
            Name = name;
            Price = price;
            ProductCategory = productCategory;
            ProductType = productType;
        }

        public override string ToString()
        {
            return $"{nameof(ProductType)} {Name}";
        }
    }
}
