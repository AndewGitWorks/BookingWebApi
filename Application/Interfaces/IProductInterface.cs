using Application.DTOs.Product;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Interfaces
{
    public interface IProductInterface
    {
        public Task CreateProductAsync(CreateProductDto request, CancellationToken cancellationToken = default);
        public Task UpdateProductAsync(UpdateProductDto product, Guid id, CancellationToken cancellationToken = default);
        public Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<List<ProductResponseDto>> GetProductsByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<ProductResponseDto> GetProductForResponseAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Product> GetProductById(Guid id, CancellationToken cancellationToken = default);
        public Task<List<ProductListResponse>> GetAllAsync(int? page = 0, int? pageSize = 0, string? search = "", decimal? minPrice = 0,
            decimal? maxPrice = 0, CancellationToken cancellationToken = default);
    }
}
