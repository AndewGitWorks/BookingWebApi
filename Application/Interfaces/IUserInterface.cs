using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserInterface
    {
        public Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task ChangeRoleAsync(CancellationToken cancellationToken = default);
        public Task ChangeEmailAsync(CancellationToken cancellationToken = default);
        public Task<User> CreateUserAsync(RegistrationRequestDto request, CancellationToken cancellationToken = default);
    }
}
