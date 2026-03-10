using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record RegistrationRequestDto(string Email, string Password, string ExtraPassword);
}
