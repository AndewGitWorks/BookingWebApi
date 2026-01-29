using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Application.Interfaces.DbInterfaces
{
    public interface IProductDbInteface
    {
        public Task AddProductAsync(Product product);
        public Task DeleteProductAsync(Guid id);
        public Task UpdateProductAsync(Product product, Guid id);
        public Task<Product> GetProductAsync(Guid id);
        public Task<List<Product>> GetByNameAsync(string name);
        public Task<Product> GetByIdAsync(Guid id);
    }
}
