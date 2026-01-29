using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsActive { get; set; } = true;
        public DateOnly CreatedAt { get; set; }
        public void DecreaseQuantity(int amount)
        {
            if(QuantityInStock < amount)
            {
                throw new Exception("Insufficient stock available.");
            }
            QuantityInStock -= amount;
        }
        public decimal GetTotalPrice(int quantity)
        {
            return Price * quantity;
        }
        
    }
}
