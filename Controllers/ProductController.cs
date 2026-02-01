using Microsoft.AspNetCore.Mvc;
using OrderFlowApi.Exceptions;
using OrderFlowApi.Mappers;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Services;
using OrderFlowApi.User;

namespace OrderFlowApi.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // create product listing
        [HttpPost]
        public async Task<IActionResult> CreateProductListing(CreateProductDto dto)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var product = await _productService.CreateProductListingAsync(dto, userId);

            return Ok(ProductMapper.ToDto(product));
        }

        // update product listing
        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProductListing(Guid productId, UpdateProductDto dto)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var product = await _productService.UpdateProductListingAsync(productId, dto, userId);
            return Ok(ProductMapper.ToDto(product));
        }

        // delete product listing
        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProductListing(Guid productId)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var product = await _productService.DeleteProductListingAsync(productId, userId);
            return Ok(ProductMapper.ToDto(product));
        }

        // view all products
        [HttpGet]
        public async Task<IActionResult> ViewProducts()
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var products = await _productService.ViewProductsAsync(userId);
            return Ok(products.Select(ProductMapper.ToDto));
        }

        // get product by id
        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Ok(ProductMapper.ToDto(product));   
        }
    }
}
