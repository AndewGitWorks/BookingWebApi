using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.CrudRepository;
using Application.DTOs.Validator;
namespace API
{
    public static class InterfaceRegistration
    {
        public static IServiceCollection ServicesRegistration(this IServiceCollection services)
        {
            // Login and Register repository
            services.AddScoped<IAuthInterface, AuthRepository>();
            // Auth registration to get token in repository
            services.AddScoped<IJwtInterface, JwtService>();
            // User repository
            services.AddScoped<IUserRepository, UserRepository>();
            // Product repository
            services.AddScoped<IProductInterface, ProductRepository>();
            // Jwt service
            services.AddScoped<JwtService>();
            services.SetValidatorHandler();
            return services;
        }
    }
}
