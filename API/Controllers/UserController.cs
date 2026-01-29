using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserDbInterface _userInterface;
        public UserController(IUserDbInterface repo)
        {
            _userInterface = repo;
        }
        //[HttpPost]
        //[Route("changerole")]
        //[Authorize(Policy = "AdminPolicy")]
        //public async Task<IActionResult> UpdateUserRole([FromBody]UpdateUserRoleDto request)
        //{
        //    await _userInterface.UpdateAsync(request);
        //    return new OkResult();
        //}
    }
}
