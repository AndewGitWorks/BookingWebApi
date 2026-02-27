using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IOrderItemDbInterface
    {
        public Task CreateAsync(Guid orderId, Product product);
        public Task DeleteAsync(Guid orderId, Guid productId);
        public Task<List<OrderItem>> GetAllAsync(Guid orderId);

    }
}
