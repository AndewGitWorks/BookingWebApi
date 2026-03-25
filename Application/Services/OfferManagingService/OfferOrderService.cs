using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OfferManagingService
{
    public class OfferOrderService : IOffedOrder
    {
        private readonly IJwtParserInterface _jwtInterface;
        private readonly IUserInterface _userInterface;
        private readonly IOrderInterface _orderInterface;
        public OfferOrderService(IJwtParserInterface jwtInterface,
            IUserInterface userInterface,
            IOrderInterface orderInterface)
        {
            _jwtInterface = jwtInterface;
            _userInterface = userInterface;
            _orderInterface = orderInterface;
        }
        public async Task CreateOffer(Guid userId, Guid orderId, CancellationToken ct)
        {
            var user = await _userInterface.GetUserByIdAsync(userId, ct);
            var order = await _orderInterface.GetOrderById(orderId, ct);
            if(user.Id != order.UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to pay for this order.");
            }
            order.Status = Domain.Enums.OrderStatus.Paid;
            await _orderInterface.UpdateOrderAsync(order);
        }
    }
}
