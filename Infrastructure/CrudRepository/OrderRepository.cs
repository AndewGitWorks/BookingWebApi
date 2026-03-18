using Application.DTOs.Order;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrudRepository
{
    public class OrderRepository : IOrderDbInterface
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task AddProductAsync(Order order, Product product, int quantity = 1)
        //{
        //    var existingOrder = await _context.Orders
        //        .Include(o => o.Items)
        //        .FirstOrDefaultAsync(o => o.Id == order.Id)
        //        ?? throw new KeyNotFoundException("Order not found");

        //    existingOrder.AddItem(product, quantity);
        //    await _context.SaveChangesAsync();
        //}

        public async Task ConfirmOrderAsync(Order order, CancellationToken token)
        {
            await _context.Orders.AddAsync(order, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteOrderAsync(Guid id, CancellationToken token)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Order not found");
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(token);
        }

        public async Task<List<OrdersListResponse>> GetByUser(Guid id, CancellationToken token)
        {
            return await _context.Orders
                .Where(x => x.UserId == id)
                .Select(x => new OrdersListResponse(
                    Id: x.Id,
                    Status: x.Status,
                    TotalAmount: x.TotalAmount,
                    CreatedAt: x.CreatedAt,
                    Items: x.Items.Select(i => new OrderItemResponse
                    {
                        ItemId = i.Id,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                ))
             .ToListAsync();
        }

        public async Task<Order?> GetDraftOrderAsync(Guid id, CancellationToken token)
        {
            return await _context.Orders.FirstOrDefaultAsync
                (x => x.Id == id && x.Status == OrderStatus.Draft)
                ?? throw new KeyNotFoundException("Draft order not found");
        }

        public async Task<Order> GetOrderAsync(Guid id, CancellationToken token)
        {
            return await _context.Orders
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(i => i.Id == id)
                ?? throw new KeyNotFoundException("Order not found");
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateOrderAsync(Order order, CancellationToken ct)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(ct);
        }

    }
}
