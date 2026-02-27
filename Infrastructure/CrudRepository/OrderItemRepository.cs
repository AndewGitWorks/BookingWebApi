using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class OrderItemRepository : IOrderItemDbInterface
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Guid orderId, Product product)
        {
            var item = new OrderItem
            {
                OrderId = orderId,
                ProductId = product.Id,
                Product = product,
            };
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();

        }

        public Task DeleteAsync(Guid orderId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderItem>> GetAllAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
