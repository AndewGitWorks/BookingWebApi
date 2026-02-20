using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class OrderService : IOrderInterface
    {
        private readonly IOrderDbInterface _order;
        private readonly IProductInterface _product;
        private readonly IJwtParserInterface _jwt;
        private readonly IUserDbInterface _user;
        public OrderService(
            IOrderDbInterface order,
            IProductInterface product,
            IJwtParserInterface jwt,
            IUserDbInterface _usr)
        {
            _order = order;
            _product = product;
            _jwt = jwt;
            _user = _usr;
        }

        public async Task AddProductAsync(string token,Guid oderId, Guid productId)
        {
            var order = await _order.GetOrderAsync(oderId);
            if(order == null)
            {
                // order = await CreateDraftAsync(token);
            }
            var product = await _product.GetByIdAsync(productId);
            order.AddItem(product, 1);
            await _order.UpdateOrderAsync(order);
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
            await _order.AddOrderAsync(newDraftCard);
            var response = new OrderResponse(newDraftCard.Id, newDraftCard.CreatedAt, newDraftCard.Status);
            return response;
        }

        public async Task CreateOrderAsync(string token)
        {
            
        }

        public Task DeleteOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllByUserAsync(string token)
        {
            var userToken = await _jwt.GetId(token);
            var orders = await _order.GetByUser(userToken);
            return orders;
        }

        public async Task<OrderDetailResponse> GetOrderDetailAsync(string token, Guid orderId)
        {
            var userToken = await _jwt.GetId(token);
            var order = await _order.GetOrderAsync(orderId) ?? throw new Exception("Order not found.");
            if(order.UserId != userToken)
            {
                throw new Exception("Unauthorized access to order details.");
            }
            var response = new OrderDetailResponse
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Items = [.. order.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                })]
            };
            return response;
        }

        public Task UpdateOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductQuantityAsync()
        {
            throw new NotImplementedException();
        }

    }
}
