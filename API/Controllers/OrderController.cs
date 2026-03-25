using System.Security.Claims;
using Application.DTOs.Order;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _order;
        private readonly IOffedOrder _offerOrder;
        private readonly IJwtParserInterface _jwt;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IJwtParserInterface jwt,
            IOrderInterface order,
            IOffedOrder offerOrder,
            ILogger<OrderController> logger
            )
        {
            _order = order;
            _offerOrder = offerOrder;
            _jwt = jwt;
            _logger = logger;
        }
        [HttpGet]
        [Route("/orders")]
        public async Task<ActionResult<OrdersListResponse>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var usrIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Token not found");
            Guid usrId = Guid.Parse(usrIdString);
            var response = await _order.GetAllByUserAsync(usrId, cancellationToken);
            return response;
        }
        [HttpPost]
        [Route("/orders/create")]
        public async Task<OrderResponse> CreateDraftAsync([FromQuery] string token, CancellationToken cancellationToken)
        {
            var response = await _order.CreateDraftAsync(token, cancellationToken);
            return response;
        }
        [HttpGet]
        [Route("/orders/{orderId}")]
        public async Task<OrderDetailResponse> GetOrderDetailsAsync([FromQuery] string token, [FromRoute] string orderId, CancellationToken cancellationToken)
        {
            var orderIdGuid = Guid.Parse(orderId);
            var response = await _order.GetOrderDetailAsync(token, orderIdGuid, cancellationToken);
            return response;
        }
        [HttpDelete]
        [Route("/orders/{orderId}/delete")]
        public async Task<IActionResult> DeleteOderAsync([FromRoute]string orderId, CancellationToken cancellationToken)
        {
            await _order.DeleteOrderAsync(Guid.Parse(orderId), cancellationToken);
            return new NoContentResult();
        }
        [Authorize]
        [HttpPost]
        [Route("/orders/{orderId}/pay")]
        public async Task<IActionResult> PayForOrderAsync([FromRoute] Guid orderId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User ID claim not found");
            _logger.LogInformation("User with ID {UserId} is attempting to pay for order with ID {OrderId}", userId, orderId);
            try
            {
                await _offerOrder.CreateOffer(Guid.Parse(userId), orderId, cancellationToken);
                return new OkResult();
            }
            catch (UnauthorizedAccessException ex)
            {
                return new UnauthorizedObjectResult(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/orders/{orderId}/items/{productId}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute]Guid orderId, [FromRoute]Guid productId, CancellationToken cancellationToken)
        {
            await _order.DeleteProductAsync(productId, orderId, cancellationToken);
            return new OkResult();
        }
        [HttpPatch]
        [Route("/orders/{orderId}/items")]
        public async Task<IActionResult> UpdateProductQuantityAsync([FromRoute]Guid orderId,
            [FromQuery]Guid productId,
            [FromQuery]int quantity,
            CancellationToken cancellationToken)
        {
            await _order.UpdateProductQuantityAsync(orderId, productId, quantity, cancellationToken);
            return new NoContentResult();
        }
        [Authorize]
        [HttpGet]
        [Route("/orders/filter")]
        public async Task<List<OrderResponse>> GetOrdersByStatusAsync([FromQuery] string status, CancellationToken cancellationToken)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var response = await _order.GetOrdersByStatusAsync(token, status, cancellationToken);
            return response;
        }
    }
}
