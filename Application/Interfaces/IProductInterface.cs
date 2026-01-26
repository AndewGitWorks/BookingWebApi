
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductInterface
    {
        public Task<bool> CreateProduct(CreateProductDto request);
        public Task<ProductResponseDto> GetProduct(string name);
    }
}
