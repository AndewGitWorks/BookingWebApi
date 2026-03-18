using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IProductDbInteface
    {
        public Task AddProductAsync(Product product, CancellationToken cancellationToken);
        public Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
        public Task UpdateProductAsync(Product product, Guid id, CancellationToken cancellationToken);
        public Task<Product> GetProductAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);
        public Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetSorted(CancellationToken cancellationToken);
    }
}
