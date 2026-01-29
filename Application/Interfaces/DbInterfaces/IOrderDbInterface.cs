using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IOrderDbInterface
    {
        public Task AddOrderAsync(Order order);
        public Task DeleteOrderAsync(Guid id);
        public Task UpdateOrderAsync(Order order);
        public Task<Order> GetOrderAsync(Guid id);
    }
}
