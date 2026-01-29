using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IUserDbInterface
    {
        public Task AddUserAsync(User usr);
        public Task DeleteUserAsync();
        public Task UpdateUserAsync();
        public Task GetUserAsync();
        public Task<User> GetByEmailAsync(string email);
        public Task<User> GetByIdAsync(Guid id);
    }
}
