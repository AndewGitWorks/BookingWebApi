using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOrderInterface
    {
        public Task CreateOrderAsync(string token);
        public Task<OrderResponse> CreateDraftAsync(string token);
        public Task AddProductAsync(string token,Guid id, Guid productId);
        public Task DeleteOrderAsync();
        public Task DeleteProductAsync();
        public Task UpdateOrderAsync();
        public Task UpdateProductQuantityAsync();
        public Task<List<Order>> GetAllByUserAsync(string token);
        public Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId);
    }
}
