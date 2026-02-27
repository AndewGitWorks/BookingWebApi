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
        public Task DeleteOrderAsync(Guid id);
        public Task DeleteProductAsync(Guid id, Guid id2);
        public Task<Order> GetOrderById(Guid id);
        public Task UpdateOrderAsync(Order order);
        public Task UpdateProductQuantityAsync(Guid orderId, Guid productId, int quantity);
        public Task<List<Order>> GetAllByUserAsync(string token);
        public Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId);
    }
}
