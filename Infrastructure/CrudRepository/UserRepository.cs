using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(RegistrationRequestDto request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            };
            var passHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.PasswordHash = passHash;
            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("User could not be created!");
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("Not Found!");
        }

        public async Task<User> GetById(Guid id)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return usr ?? throw new Exception("Not Found!");
        }

        public async Task Update(UpdateUserRoleDto request)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email) ?? throw new Exception("User not found");
            usr.Role = request.Role;
            _context.Users.Update(usr);
            await _context.SaveChangesAsync();
        }
    }
}
