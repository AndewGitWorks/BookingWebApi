using API.Models;
using Application.DTOs.User;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public sealed record TokenResponse(string Token);
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
        public async Task<ActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {

            try
            {
                var token = await _authInterface.Login(request, cancellationToken);
                //var liveToken = HttpContext.Request.Cookies.Any(x => x.Key == "myToken").ToString();
                //if (String.IsNullOrEmpty(liveToken))
                //{
                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                    {
                        HttpOnly = true
                    });
                //}
                var email = request.Email;
                _logger.LogInformation("User logged in with email: {Email}, at {DateTime.Utc.Now}", email, DateTime.UtcNow);
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
        public async Task<ActionResult<TokenResponse>> Registration([FromBody] RegistrationRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request, cancellationToken);
                var token = await _authInterface.Registration(request, cancellationToken);
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true
                });
                var response = new TokenResponse(token);
                _logger.LogInformation("User registered with email: {Email}", request.Email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Registration Error @{ex.Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout([FromQuery] string token, CancellationToken cancellationToken)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Cookies.Delete("token");
                
                var requestEmail = await _jwt.GetEmailFromClaimAsync(token, cancellationToken);
                _logger.LogInformation("User with email: {Email} logged out", requestEmail);
                
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging out");
                return StatusCode(500, "An error occurred while logging out.");
            }
        }
    }
}
