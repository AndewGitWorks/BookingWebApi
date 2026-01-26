using Domain.Entities;
using Infrastructure.Persistance.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbContext());
            modelBuilder.ApplyConfiguration(new ProductDbContext());
            modelBuilder.ApplyConfiguration(new OrderItemDbContext());
            modelBuilder.ApplyConfiguration(new OrderDbContext());
        }
    }
}
