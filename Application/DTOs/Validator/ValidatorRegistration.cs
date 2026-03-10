using Application.DTOs.Product;
using Application.DTOs.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Validator
{
    public static class ValidatorRegistration
    {
        public static IServiceCollection SetValidatorHandler(this IServiceCollection service)
        {
            service.AddSingleton<IValidator<RegistrationRequestDto>, CreateUserRequestValidator>();
            service.AddSingleton<IValidator<LoginRequestDto>, LoginRequestValidator>();
            service.AddSingleton<IValidator<CreateProductDto>, CreateProductRequestValidator>();
            service.AddSingleton<IValidator<UpdateUserRoleDto>, UpdateUserRoleValidator>();
            return service;
        }
    }
}
