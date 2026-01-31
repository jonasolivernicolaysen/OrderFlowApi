using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Models;
using Microsoft.EntityFrameworkCore;
using OrderFlowApi.Mappers;
using OrderFlowApi.Exceptions;

namespace OrderFlowApi.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // create product listing
        public async Task<ProductModel> CreateProductListingAsync(CreateProductDto dto)
        {
            var product = new ProductModel
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // update product listing
        public async Task<bool> UpdateProductListingAsync(ProductModel dto)
        {
            var product = _context.Products.Find(dto.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(dto.ProductId);
            }
            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Description = dto.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        // delete product listing
        public async Task<bool> DeleteProductListingAsync(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // view all product listings
        public async Task<bool> ViewProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
            {
                return false;
            }
            return true;
        }

        // get product by id
        public async Task<bool> ViewProductsAsync(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            return true;
        }
    }
}