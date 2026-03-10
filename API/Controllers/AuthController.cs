using API.Models;
using Application.DTOs.User;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        private readonly IValidator<RegistrationRequestDto> _validator;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtParserInterface _jwt;
        public AuthController(IAuthInterface authInterface,
            IValidator<RegistrationRequestDto> validator,
            ILogger<AuthController> logger,
            IJwtParserInterface parser)
        {
            _authInterface = authInterface;
            _validator = validator;
            _logger = logger;
            _jwt = parser;
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            try
            {
                var token = await _authInterface.Login(request);
                var liveToken = HttpContext.Request.Cookies.Any(x => x.Key == "myToken");
                if (liveToken) { }
                else
                {
                    HttpContext.Response.Cookies.Append("myToken", token, new CookieOptions
                    {
                        HttpOnly = true
                    });
                }
                var email = request.Email;
                _logger.LogInformation("User logged in with email: {Email}", email);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                _logger.LogError("Error @{ex.Message} User with @{request.Email}", ex, request.Email);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        [Route("/registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestDto request)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);
                var token = await _authInterface.Registration(request);
                HttpContext.Response.Cookies.Append("myToken", token, new CookieOptions
                {
                    HttpOnly = true
                });
                _logger.LogInformation("User registered with email: {Email}", request.Email);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Registration Error @{ex.Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        [Route("/logout")]
        public async Task Logout([FromQuery] string token)
        {
            try
            {
                HttpContext.Response.Cookies.Delete("myToken");
                var requestEmail = await _jwt.GetEmailFromClaimAsync(token);
                _logger.LogInformation("User with email: @{requestEmail}", requestEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging out.");
            }
        }
    }
}
