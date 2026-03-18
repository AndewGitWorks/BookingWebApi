using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.DbInterfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrudRepository
{
    public class UserRepository : IUserDbInterface
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User usr, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(usr, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteUserAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken) ?? throw new Exception("User not found");
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new Exception("User not found");
        }

        public Task GetUserAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
