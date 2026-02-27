using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class OrderService : IOrderInterface
    {
        private readonly IOrderDbInterface _order;
        private readonly IProductInterface _product;
        private readonly IJwtParserInterface _jwt;
        private readonly IUserDbInterface _user;
        private readonly IOrderItemDbInterface _orderItem;
        public OrderService(
            IOrderDbInterface order,
            IProductInterface product,
            IJwtParserInterface jwt,
            IUserDbInterface _usr,
            IOrderItemDbInterface orderItem)
        {
            _order = order;
            _product = product;
            _jwt = jwt;
            _user = _usr;
            _orderItem = orderItem;
        }

        public async Task AddProductAsync(string token, Guid orderId, Guid productId)
        {
            var order = await _order.GetOrderAsync(orderId);
            if (order == null)
            {
                order = await _order.GetDraftOrderAsync(orderId)
                    ?? throw new Exception("Draft order not found");
            }
            var product = await _product.GetByIdAsync(productId);
            
            order.AddItem(product, 1);
            await _order.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<OrderResponse> CreateDraftAsync(string token)
        {
            var userId = await _jwt.GetId(token);
            var actualUser = await _user.GetByIdAsync(userId);
            var newDraftCard = new Order
            {
                UserId = actualUser.Id,
                User = actualUser,
                Status = OrderStatus.Draft,
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };
            await _order.ConfirmOrderAsync(newDraftCard);
            var response = new OrderResponse(newDraftCard.Id, newDraftCard.CreatedAt, newDraftCard.Status);
            return response;
        }

        public async Task CreateOrderAsync(string token)
        {
            // Implementation continues...
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            await _order.DeleteOrderAsync(id);
        }

        public async Task DeleteProductAsync(Guid productId, Guid orderId)
        {
            await _orderItem.DeleteAsync(productId, orderId);
        }

        public Task<List<Order>> GetAllByUserAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId)
        {
            var dbOrder = await _order.GetOrderAsync(orderId);
            var response = new OrderDetailResponse
            {
                Id = dbOrder.Id,
                CreatedAt = dbOrder.CreatedAt,
                Status = dbOrder.Status,
                TotalAmount = dbOrder.TotalAmount,
                Items = dbOrder.Items.Select
                (x => new OrderItemResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product!.Name ?? string.Empty,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                }).ToList()
            } ?? throw new Exception("Order not found");
            return response;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var item = await _order.GetOrderAsync(order.Id);
        }

        public async Task UpdateProductQuantityAsync(Guid orderId, Guid productId, int quantity)
        {
            var item = await _order.GetOrderAsync(orderId) 
                ?? throw new Exception("Order not found");
            var product = item.Items.FirstOrDefault(
                x => x.ProductId == productId)
                ?? throw new Exception("No product in order");
            product.UpdateQuantity(quantity);
            await _order.SaveChangesAsync(CancellationToken.None);
        }
    }
}
