using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Products.DTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductType ProductType { get; set; }
    }
}
