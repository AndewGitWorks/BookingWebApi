using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class ProductRepository : IProductInterface
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProduct(CreateProductDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.Quantity,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductResponseDto> GetProduct(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name) ?? throw new Exception("Not found");
            var response = new ProductResponseDto(
            Title: product.Name,
            Description: product.Description,
            Price: product.Price,
            Quantity: product.QuantityInStock
            );
            return response;
        }
    }
}
