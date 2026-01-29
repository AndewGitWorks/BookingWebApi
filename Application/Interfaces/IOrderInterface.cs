using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOrderInterface
    {
        public Task CreateOrderAsync();
        public Task CreateDraftAsync(string token);
        public Task AddProductAsync(Guid id, Guid productId);
        public Task DeleteOrderAsync();
        public Task DeleteProductAsync();
        public Task UpdateOrderAsync();
        public Task UpdateProductQuantityAsync();

    }
}
