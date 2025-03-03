using FunBooksAndVideos.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.Application.Products.DTOs
{
    public class ProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
    }
}
