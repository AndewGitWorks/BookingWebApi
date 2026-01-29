using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Services
{
    public class AuthService : IAuthInterface
    {
        private readonly IUserDbInterface _userRepository;
        private readonly IJwtInterface _jwtInterface;
        private readonly IUserInterface _usr;
        public AuthService(IUserDbInterface userRepository,
            IJwtInterface jwtInterface,
            IUserInterface usr)
        {
            _userRepository = userRepository;
            _jwtInterface = jwtInterface;
            _usr = usr;
        }
        public async Task<string> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            var requestHash = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (requestHash == PasswordVerificationResult.Success)
            {
                return _jwtInterface.GenerateToken(user);
            }
            throw new Exception("Unauthorized! Wrong password or email");
        }

        public async Task<string> Registration(RegistrationRequestDto request)
        {
            if(request.Password != request.ExtraPassword)
            {
                throw new Exception("Passwords do not match");
            }
            var newUser = await _usr.CreateUserAsync(request);
            var token = _jwtInterface.GenerateToken(newUser);
            return token;
        }
    }
}
