using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController
    {
        private readonly IJwtParserInterface _jwt;
        private readonly IOrderInterface _order;
        public OrderController(IJwtParserInterface jwt, IOrderInterface order)
        {
            _jwt = jwt;
            _order = order;
        }
        [HttpGet]
        [Route("/cart")]
        public async Task<ICollection<Order>> GetAllOrdersAsync([FromQuery]string token)
        {

            var response = await _order.GetAllByUserAsync(token);
            return response;

        }
        [HttpPost]
        [Route("/cart/create")]
        public async Task<OrderResponse> CreateDraftAsync([FromQuery] string token)
        {
            var response = await _order.CreateDraftAsync(token);
            return response;
        }
        [HttpGet]
        [Route("/cart/{orderId}")]
        public async Task<OrderDetailResponse> GetOrderDetailsAsync([FromQuery] string token, [FromRoute] string orderId)
        {
            var orderIdGuid = Guid.Parse(orderId);
            var response = await _order.GetOrderDetailAsync(token, orderIdGuid);
            return response;
        }
    }
}
