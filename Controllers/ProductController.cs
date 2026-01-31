using Microsoft.AspNetCore.Mvc;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Services;


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
        public async Task<IActionResult> CreateProductListing(CreateProductDto product)
        {
            // var result = _productService.CreateProductListing
        }
    }
}
