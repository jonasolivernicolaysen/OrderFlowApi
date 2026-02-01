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
            var product = ProductMapper.ToProductModel(dto, userId);
            if (product == null)
                throw new BadProductlistingException("Product listing if insufficcient");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // update product listing
        public async Task<ProductModel> UpdateProductListingAsync(Guid productId, UpdateProductDto dto, int userId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            if (product.CreatedByUserId != userId)
                throw new UserNotAuthorizedException();

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Description = dto.Description
                ;
            await _context.SaveChangesAsync();
            return product;
        }

        // delete product listing
        public async Task<ProductModel> DeleteProductListingAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // view all product listings
        public async Task<List<ProductModel>> ViewProductsAsync(int userId)
        {
            var products = await _context.Products.
                Where(p => p.CreatedByUserId == userId).ToListAsync();

            if (products == null || products.Count == 0)
            {
                throw new NoProductsException("No products found");
            }
            return products;
        }

        // get product by id
        public async Task<ProductModel> GetProductAsync(Guid productId)
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