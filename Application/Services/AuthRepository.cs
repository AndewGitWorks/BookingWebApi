using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Services
{
    public class AuthRepository : IAuthInterface
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtInterface _jwtInterface;
        public AuthRepository(IUserRepository userRepository, IJwtInterface jwtInterface)
        {
            _userRepository = userRepository;
            _jwtInterface = jwtInterface;
        }
        public async Task<string> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmail(request.Email);
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
            var newUser = await _userRepository.CreateUser(request);
            var token = _jwtInterface.GenerateToken(newUser);
            return token;
        }
    }
}
