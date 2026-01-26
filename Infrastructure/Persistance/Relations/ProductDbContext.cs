using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Relations
{
    public class ProductDbContext : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(p => p.Description)
                   .HasMaxLength(1000);
            builder.Property(p => p.Price)
                   .IsRequired();
        }
    }
}
