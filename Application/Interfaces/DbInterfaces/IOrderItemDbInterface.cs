using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IOrderItemDbInterface
    {
        public Task CreateAsync(Guid orderId, Product product, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid orderId, Guid productId, CancellationToken cancellationToken);
        //public Task<List<OrderItem>> GetAllAsync(Guid orderId);

    }
}
