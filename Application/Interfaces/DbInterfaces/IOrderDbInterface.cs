using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IOrderDbInterface
    {
        public Task ConfirmOrderAsync(Order order);
        public Task DeleteOrderAsync(Guid id);
        public Task UpdateOrderAsync(Order order);
        public Task<Order> GetOrderAsync(Guid id);
        public Task<Order?> GetDraftOrderAsync(Guid id);
        public Task<List<Order>> GetByUser(Guid id);
    }
}
