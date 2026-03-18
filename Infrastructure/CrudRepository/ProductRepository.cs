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

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new NotImplementedException();
            _context.Products.Remove(request);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _context.GetHashCode();
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new NotImplementedException();
        }

        public async Task<List<Product>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(name)).AsNoTracking()
                .ToListAsync(cancellationToken);
            return products;
        }

        public async Task<Product> GetProductAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetSorted(CancellationToken cancellationToken)
        {
            return new List<Product>();
        }

        public async Task UpdateProductAsync(Product product, Guid id, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
