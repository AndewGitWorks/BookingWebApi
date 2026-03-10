using Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Validator
{
    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleDto>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Matches("^[a-zA-Z]+$").WithMessage("Role must contain only letters.");
        }
    }
}
