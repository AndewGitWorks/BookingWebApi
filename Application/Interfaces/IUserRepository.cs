using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetById(Guid id);
        public Task<User> GetByEmail(string email);
        public Task<User> CreateUser(RegistrationRequestDto request);
        public Task Update(UpdateUserRoleDto request);
    }
}
