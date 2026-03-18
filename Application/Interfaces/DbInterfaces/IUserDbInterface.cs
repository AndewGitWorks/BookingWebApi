using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IUserDbInterface
    {
        public Task AddUserAsync(User usr, CancellationToken cancellationToken);
        public Task DeleteUserAsync(CancellationToken cancellationToken);
        public Task UpdateUserAsync(CancellationToken cancellationToken);
        public Task GetUserAsync(CancellationToken cancellationToken);
        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
