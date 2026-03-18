using Application.DTOs.Order;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOrderInterface
    {
        public Task CreateOrderAsync(string token, CancellationToken cancellationToken = default);
        public Task<OrderResponse> CreateDraftAsync(string token, CancellationToken cancellationToken = default);
        public Task AddProductAsync(string token, Guid id, Guid productId, CancellationToken cancellationToken = default);
        public Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
        public Task DeleteProductAsync(Guid id, Guid id2, CancellationToken cancellationToken = default);
        public Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken = default);
        public Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
        public Task UpdateProductQuantityAsync(Guid orderId, Guid productId, int quantity, CancellationToken cancellationToken = default);
        public Task<List<OrdersListResponse>> GetAllByUserAsync(string token, CancellationToken cancellationToken = default);
        public Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId, CancellationToken cancellationToken = default);
    }
}
