using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class ProductRepository : IProductDbInteface
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var request = await _context.Products.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotImplementedException();
            _context.Products.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            _context.GetHashCode();
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotImplementedException();
        }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
            return products;
        }

        public Task<Product> GetProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
