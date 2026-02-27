using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserInterface
    {
        public Task<User> GetUserByIdAsync(Guid id);
        public Task ChangeRoleAsync();
        public Task ChangeEmailAsync();
        public Task<User> CreateUserAsync(RegistrationRequestDto request);
    }
}
