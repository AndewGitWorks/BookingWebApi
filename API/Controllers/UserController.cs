using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserRepository _userInterface;
        public UserController(IUserRepository repo)
        {
            _userInterface = repo;
        }
        [HttpPost]
        [Route("changerole")]
        public async Task<IActionResult> UpdateUserRole([FromBody]UpdateUserRoleDto request)
        {
            await _userInterface.Update(request);
            return new OkResult();
        }
    }
}
