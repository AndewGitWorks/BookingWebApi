using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record RegistrationRequestDto(string Email, string Password, string ExtraPassword);
}
