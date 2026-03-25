using Application.DTOs.Order;
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

        public async Task AddProductAsync(string token, Guid orderId, Guid productId, CancellationToken cancellationToken = default)
        {
            var order = await _order.GetOrderAsync(orderId, cancellationToken);
            if (order == null)
            {
                order = await _order.GetDraftOrderAsync(orderId, cancellationToken)
                    ?? throw new Exception("Draft order not found");
            }
            var product = await _product.GetProductById(productId, cancellationToken);
            
            order.AddItem(product, 1);
            await _order.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderResponse> CreateDraftAsync(string token, CancellationToken cancellationToken = default)
        {
            var userId = await _jwt.GetId(token);
            var actualUser = await _user.GetByIdAsync(userId, cancellationToken);
            var newDraftCard = new Order
            {
                UserId = actualUser.Id,
                User = actualUser,
                Status = OrderStatus.Draft,
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };
            await _order.ConfirmOrderAsync(newDraftCard, cancellationToken);
            var response = new OrderResponse
            {
                Id = newDraftCard.Id,
                CreatedAt = newDraftCard.CreatedAt,
                Status = nameof(newDraftCard.Status)
            };
            return response;
        }

        public async Task CreateOrderAsync(string token, CancellationToken cancellationToken = default)
        {
            // Implementation continues...
        }

        public async Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _order.DeleteOrderAsync(id, cancellationToken);
        }

        public async Task DeleteProductAsync(Guid productId, Guid orderId, CancellationToken cancellationToken = default)
        {
            await _orderItem.DeleteAsync(productId, orderId, cancellationToken);
        }

        public async Task<OrdersListResponse> GetAllByUserAsync(Guid id, CancellationToken cancellationToken = default)
        { 
            var items = await _order.GetByUser(id, cancellationToken);
            return items ?? throw new Exception("Cart is empty");
        }

        public Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId, CancellationToken cancellationToken = default)
        {
            var dbOrder = await _order.GetOrderAsync(orderId, cancellationToken);
            var response = new OrderDetailResponse
            {
                Id = dbOrder.Id,
                CreatedAt = dbOrder.CreatedAt,
                Status = dbOrder.Status,
                TotalAmount = dbOrder.TotalAmount,
                Items = dbOrder.Items.Select
                (x => new OrderItemResponse
                {
                    ItemId = x.ProductId,
                    ProductName = x.Product!.Name ?? string.Empty,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                }).ToList()
            } ?? throw new Exception("Order not found");
            return response;
        }

        public async Task<List<OrderResponse>> GetOrdersByStatusAsync(string token, string status, CancellationToken cancellationToken = default)
        {
            var usrId = await _jwt.GetId(token);
            var orderStatus = Enum.Parse<OrderStatus>(status);
            var response = await _order.GetOrdersByStatusAsync(usrId, orderStatus, cancellationToken);
            return response;
        }

        public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            var item = await _order.GetOrderAsync(order.Id, cancellationToken);
        }

        public async Task UpdateProductQuantityAsync(Guid orderId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var item = await _order.GetOrderAsync(orderId, cancellationToken) 
                ?? throw new Exception("Order not found");
            var product = item.Items.FirstOrDefault(
                x => x.ProductId == productId)
                ?? throw new Exception("No product in order");
            product.UpdateQuantity(quantity);
            await _order.SaveChangesAsync(cancellationToken);
        }
    }
}
