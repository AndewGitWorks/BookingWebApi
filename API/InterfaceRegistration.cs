using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.CrudRepository;
using Application.DTOs.Validator;
using Application.Interfaces.DbInterfaces;
using Application.Services.OfferManagingService;
namespace API
{
    public static class InterfaceRegistration
    {
        public static IServiceCollection ServicesRegistration(this IServiceCollection services)
        {
            // Login and Register repository
            services.AddScoped<IAuthInterface, AuthService>();
            // Auth registration to get token in repository
            services.AddScoped<IJwtInterface, JwtService>();
            // User repository
            services.AddScoped<IUserDbInterface, UserRepository>();
            // Product repository
            services.AddScoped<IProductDbInteface, ProductRepository>();
            // Jwt service
            services.AddScoped<JwtService>();
            // Jwt handler or extractor
            services.AddScoped<IJwtParserInterface, JwtParserService>();
            // Minimal fluent validation
            services.SetValidatorHandler();
            // Order repository
            services.AddScoped<IOrderDbInterface, OrderRepository>();
            // Order service
            services.AddScoped<IOrderInterface, OrderService>();
            // Product service
            services.AddScoped<IProductInterface, ProductService>();
            // User service
            services.AddScoped<IUserInterface, UserService>();
            // Order offer service
            services.AddScoped<IOffedOrder, OfferOrderService>();
            // OrderItem repository
            services.AddScoped<IOrderItemDbInterface, OrderItemRepository>();
            return services;
        }
    }
}
