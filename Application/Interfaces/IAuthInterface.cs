using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthInterface
    {
        public Task<string> Login(LoginRequestDto request);
        public Task<string> Registration(RegistrationRequestDto request);
    }
}
