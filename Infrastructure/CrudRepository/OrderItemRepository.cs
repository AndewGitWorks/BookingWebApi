using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(Guid orderId, Guid productId)
        {
            var item = await _context.OrderItems.FirstOrDefaultAsync(
                x => x.ProductId == productId && 
                x.OrderId == orderId) ?? throw new Exception("Cannot remove, not found!");
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<OrderItem>> GetAllAsync(Guid orderId)
        //{
        //    return 
        //         await _context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
        //}
    }
}
