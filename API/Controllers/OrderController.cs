using Application.DTOs;
using Application.Interfaces;
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
        //[HttpPost]
        //[Route("gettokentest")]
        //public async Task<IActionResult> CreateOrder([FromBody]CreateOrderRequest request,[FromQuery]string token)
        //{
        //    var tokenGuid = await _jwt.GetId(token);
        //    await _order.CreateOrder(tokenGuid, request.Items);
        //    return new OkResult();
        //}
        //[HttpPost]
        //[Route("add")]
        //public async Task<IActionResult> AddToOrder([FromBody] , [FromQuery] string token)
        //{
        //    return BadRequest();
        //}
    }
}
