using Application.DTOs.User;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserInterface
    {
        private readonly IUserDbInterface _userRepository;
        public UserService(IUserDbInterface user)
        {
            _userRepository = user;
        }
        public Task ChangeEmailAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ChangeRoleAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateUserAsync(RegistrationRequestDto request, CancellationToken cancellationToken = default)
        {
            if(request.Password != request.ExtraPassword)
            {
                throw new ArgumentException("Passwords do not match");
            }
            var usr = new User
            {
                Email = request.Email,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                Orders = [],
                Role = "User"
            };
            var passHashed = new PasswordHasher<User>().HashPassword(usr, request.Password);
            usr.PasswordHash = passHashed;
            await _userRepository.AddUserAsync(usr, cancellationToken);
            return usr;
        }

        public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("User not found");
        }
    }
}
