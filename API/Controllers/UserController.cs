using Application.DTOs;
using Application.DTOs.Order;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDbInterface _userInterface;
        private readonly IOrderInterface _orderInterface;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtParserInterface _jwtParserInterface;
        public UserController(IUserDbInterface repo,
            IOrderInterface orderInterface,
            ILogger<UserController> logger,
            IJwtParserInterface jwtParserInterface
            )
        {
            _userInterface = repo;
            _orderInterface = orderInterface;
            _logger = logger;
            _jwtParserInterface = jwtParserInterface;
        }
        //[HttpPost]
        //[Route("changerole")]
        //[Authorize(Policy = "AdminPolicy")]
        //public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto request)
        //{
        //    await _userInterface.UpdateAsync(request);
        //    return new OkResult();
        //}
        // [HttpGet]
        // [Route("order/list")]
        // public async Task<List<OrdersListResponse>> GetAllOrdersAsync([FromQuery]string token)
        // {
        //     var result = await _jwtParserInterface.GetId(token);
        //     var request = await _userInterface.GetByIdAsync(result);
        //     var response = await _orderInterface.GetAllByUserAsync(token);
        //     _logger.LogInformation("User: {@request} Action: Show all orders", request);
        //     return response;
        // }
    }
}
