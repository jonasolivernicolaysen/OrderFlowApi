using Microsoft.EntityFrameworkCore;
using OrderFlowApi.Exceptions;
using OrderFlowApi.Mappers;
using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.User;

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
        public async Task<ProductModel> CreateProductListingAsync(CreateProductDto dto, int userId)
        {
            if (string.IsNullOrEmpty(dto.ProductName))
                throw new BadProductListingException("Product name cannot be empty");

            if (dto.Price <= 0)
                throw new BadProductListingException("Price cannot be less than zero");

            var product = ProductMapper.ToProductModel(dto, userId);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // update product listing
        public async Task<ProductModel> UpdateProductListingAsync(Guid productId, UpdateProductDto dto, int userId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new ProductNotFoundException(productId);
            
            if (product.CreatedByUserId != userId)
                throw new UserNotAuthorizedException();

            if (string.IsNullOrEmpty(dto.ProductName))
                throw new BadProductListingException("Product name cannot be empty");

            if (dto.Price <= 0)
                throw new BadProductListingException("Price cannot be less than zero");

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.LastUpdatedAt= DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return product;
        }

        // delete product listing
        public async Task<ProductModel> DeleteProductListingAsync(Guid productId, int userId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new ProductNotFoundException(productId);
            
            if (product.CreatedByUserId != userId)
                throw new UserNotAuthorizedException();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // view all product listings
        public async Task<List<ProductModel>> ViewProductsAsync(int userId)
        {
            var products = await _context.Products.
                Where(p => p.CreatedByUserId == userId).ToListAsync();

            return products;
        }

        // get product by id
        public async Task<ProductModel> GetProductByIdAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }
            return product;
        }
    }
}