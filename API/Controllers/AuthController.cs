using API.Models;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto request)
        {
            var token = await _authInterface.Login(request);
            HttpContext.Response.Cookies.Append("myToken", token, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(token);
        }
        [HttpPost]
        [Route("/registration")]
        public async Task<IActionResult> Registration([FromBody]RegistrationRequestDto request)
        { 
            var token = await _authInterface.Registration(request);
            HttpContext.Response.Cookies.Append("myToken", token, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(token);
        }
    }
}
