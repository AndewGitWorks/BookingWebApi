using Application.DTOs.Product;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Application.Services
{
    public class ProductService : IProductInterface
    {
        private readonly IProductDbInteface _productRepository;
        public ProductService(IProductDbInteface product)
        {
            _productRepository = product;
        }
        public async Task CreateProductAsync(CreateProductDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.Quantity
            };
            await _productRepository.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<ProductListResponse>> GetAllAsync(int? page = 0, int? pageSize = 0, string? search = "", decimal? minPrice = 0,
            decimal? maxPrice = 0)
        {
            var list = await _productRepository.GetAllAsync();
            var response = list.Select(x => new ProductListResponse
            (
                Id: x.Id,
                Name: x.Name,
                Description: x.Description,
                Price: x.Price,
                Quantity: x.QuantityInStock
            )).ToList();
            return response;
        }

        public Task<Product> GetProductById(Guid id)
        {
            var item = _productRepository.GetProductAsync(id);
            return item ?? throw new Exception("Product not found!");
        }

        public async Task<ProductResponseDto> GetProductForResponseAsync(Guid id)
        {
            var item = await _productRepository.GetProductAsync(id);
            return new ProductResponseDto
            (
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                Price: item.Price,
                Quantity: item.QuantityInStock
            );
        }

        public async Task<List<ProductResponseDto>> GetProductsByNameAsync(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            var response = products.Select(p => new ProductResponseDto
            (
                Id: p.Id,
                Name: p.Name,
                Description: p.Description,
                Price: p.Price,
                Quantity: p.QuantityInStock
            )).ToList();
            return response;
        }

        public async Task UpdateProductAsync(UpdateProductDto product,Guid id)
        {
            var existingProduct = await _productRepository.GetProductAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found!");
            }
            existingProduct.Name = product.Name ?? existingProduct.Name;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.Price = product.Price ?? existingProduct.Price;
            existingProduct.QuantityInStock = product.Quantity ?? existingProduct.QuantityInStock;
            await _productRepository.UpdateProductAsync(existingProduct ,id);
        }
    }
}
