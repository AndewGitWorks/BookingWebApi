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

        public async Task AddProductAsync(Guid oderId, Guid productId)
        {
            var order = await _order.GetOrderAsync(oderId);
            var product = await _product.GetByIdAsync(productId);
            order.AddItem(product, 1);
            await _order.UpdateOrderAsync(order);
        }

        public async Task CreateDraftAsync(string token)
        {
            var userId = await _jwt.GetId(token);
            var actualUser = await _user.GetByIdAsync(userId);
            var newDraftCard = new Order
            {
                Id = userId,
                User = actualUser,
                Status = OrderStatus.Draft,
            };
            await _order.AddOrderAsync(newDraftCard);
        }

        public Task CreateOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync()
        {
            throw new NotImplementedException();
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
