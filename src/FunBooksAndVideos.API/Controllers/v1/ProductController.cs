using Asp.Versioning;
using FunBooksAndVideos.API.Models;
using FunBooksAndVideos.Application.Products.DTOs;
using FunBooksAndVideos.Application.Products.Interfaces;
using FunBooksAndVideos.Domain.Entities.ShopItems;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion(1.0)]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = await _productService.GetAll();
            var dto = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ProductCategory = p.ProductCategory,
                ProductType = p.ProductType
            });
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            var dto = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductCategory = product.ProductCategory,
                ProductType = product.ProductType
            };
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">Product details</param>
        /// <returns>The created product's ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Create([FromBody] ProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Invalid request model: {Errors}", string.Join(", ", errors));
                return BadRequest(new ErrorResponse("Validation failed", errors));
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                ProductCategory = productDto.ProductCategory,
                ProductType = productDto.ProductType
            };

            var productId = await _productService.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="productDto">Updated product details</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Invalid request model: {Errors}", string.Join(", ", errors));
                return BadRequest(new ErrorResponse("Validation failed", errors));
            }

            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.ProductCategory = productDto.ProductCategory;
            product.ProductType = productDto.ProductType;

            await _productService.Update(product);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            await _productService.Delete(product);
            return NoContent();
        }
    }
}
