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
            service.AddScoped<IValidator<RegistrationRequestDto>, CreateUserRequestValidator>();
            return service;
        }
    }
}
