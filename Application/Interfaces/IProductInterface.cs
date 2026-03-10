using Application.DTOs.Product;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Interfaces
{
    public interface IProductInterface
    {
        public Task CreateProductAsync(CreateProductDto request);
        public Task UpdateProductAsync(Product product,Guid id);
        public Task DeleteProductAsync(Guid id);
        public Task<List<ProductResponseDto>> GetProductsByNameAsync(string name);
        public Task<ProductResponseDto> GetProductForResponseAsync(Guid id);
        public Task<Product> GetProductById(Guid id);
        public Task<List<ProductListResponse>> GetAllAsync();
    }
}
