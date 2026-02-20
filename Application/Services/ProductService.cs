using Application.DTOs;
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

        public async Task<List<ProductListResponse>> GetAllAsync()
        {
            var list = await _productRepository.GetAllAsync();
            var response = list.Select(x => new ProductListResponse
            (
                Name: x.Name,
                Description: x.Description,
                Price: x.Price,
                Quantity: x.QuantityInStock
            )).ToList();
            return response;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<List<GetProductByName>> GetProductsByNameAsync(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            var response = products.Select(p => new GetProductByName
            (
                Name: p.Name,
                Description: p.Description,
                Price: p.Price,
                Quantity: p.QuantityInStock
            )).ToList();
            return response;
        }

        public async Task UpdateProductAsync(Product product,Guid id)
        {
            await _productRepository.UpdateProductAsync(product ,id);
        }
    }
}
