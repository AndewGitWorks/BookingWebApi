using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Migrations;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class OrderRepository : IOrderDbInterface
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Task DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _context.Orders
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("Order not found");
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            entity = order;
            await _context.SaveChangesAsync();
        }
    }
}
