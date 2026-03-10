using Application.DTOs.Order;
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
        private readonly IOrderInterface _order;
        private readonly IOffedOrder _offerOrder;
        public OrderController(IJwtParserInterface jwt,
            IOrderInterface order,
            IOffedOrder offerOrder)
        {
            _order = order;
            _offerOrder = offerOrder;
        }
        [HttpGet]
        [Route("/order")]
        public async Task<ICollection<OrdersListResponse>> GetAllOrdersAsync([FromQuery]string token)
        {
            var response = await _order.GetAllByUserAsync(token);
            return response;
        }
        [HttpPost]
        [Route("/order/create")]
        public async Task<OrderResponse> CreateDraftAsync([FromQuery] string token)
        {
            var response = await _order.CreateDraftAsync(token);
            return response;
        }
        [HttpGet]
        [Route("/order/{orderId}")]
        public async Task<OrderDetailResponse> GetOrderDetailsAsync([FromQuery] string token, [FromRoute] string orderId)
        {
            var orderIdGuid = Guid.Parse(orderId);
            var response = await _order.GetOrderDetailAsync(token, orderIdGuid);
            return response;
        }
        [HttpDelete]
        [Route("/order/{orderId}/delete")]
        public async Task<IActionResult> DeleteOderAsync([FromRoute]string orderId)
        {
            await _order.DeleteOrderAsync(Guid.Parse(orderId));
            return new NoContentResult();
        }
        [HttpPost]
        [Route("/order/{orderId}/pay")]
        public async Task<IActionResult> PayForOrderAsync([FromQuery] string token, [FromRoute] Guid orderId)
        {
            try
            {
                await _offerOrder.CreateOffer(token, orderId);
                return new OkResult();
            }
            catch (UnauthorizedAccessException ex)
            {
                return new UnauthorizedObjectResult(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/order/{orderId}/items/{productId}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute]Guid orderId, [FromRoute]Guid productId)
        {
            await _order.DeleteProductAsync(productId, orderId);
            return new OkResult();
        }
        [HttpPatch]
        [Route("/order/{orderId}/items")]
        public async Task<IActionResult> UpdateProductQuantityAsync([FromRoute]Guid orderId,
            [FromQuery]Guid productId,
            [FromQuery]int quantity)
        {
            await _order.UpdateProductQuantityAsync(orderId, productId, quantity);
            return new NoContentResult();
        }
    }
}
