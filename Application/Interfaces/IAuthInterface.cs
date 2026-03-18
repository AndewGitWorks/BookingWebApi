using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthInterface
    {
        public Task<string> Login(LoginRequestDto request, CancellationToken cancellationToken = default);
        public Task<string> Registration(RegistrationRequestDto request, CancellationToken cancellationToken = default);
    }
}
