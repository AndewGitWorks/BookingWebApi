
using Application.DTOs;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Interfaces
{
    public interface IProductInterface
    {
        public Task CreateProductAsync(CreateProductDto request);
        public Task UpdateProductAsync();
        public Task DeleteProductAsync();
        public Task<List<GetProductByName>> GetProductsByNameAsync(string name);
        public Task<Product> GetByIdAsync(Guid id);
    }
}
