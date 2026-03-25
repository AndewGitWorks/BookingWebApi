using Application.DTOs.Order;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IOrderDbInterface
    {
        public Task ConfirmOrderAsync(Order order, CancellationToken token);
        public Task DeleteOrderAsync(Guid id, CancellationToken token);
        public Task UpdateOrderAsync(Order order, CancellationToken ct);
        public Task<Order> GetOrderAsync(Guid id, CancellationToken token);
        public Task<Order?> GetDraftOrderAsync(Guid id, CancellationToken token);
        public Task<OrdersListResponse> GetByUser(Guid id, CancellationToken token);
        public Task SaveChangesAsync(CancellationToken ct);
        public Task<List<OrderResponse>> GetOrdersByStatusAsync(Guid userId, OrderStatus status, CancellationToken token);
        //public Task AddProductAsync(Order order,Product product, int quantity);
    }
}
