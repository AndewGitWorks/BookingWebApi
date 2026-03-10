using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public void UpdateQuantity(int amount)
        {
            //if (this.Quantity < amount)
            //{
            //    throw new Exception("Insufficient stock available.");
            //}
            if (amount >= int.MaxValue || amount <= int.MinValue) 
                throw new Exception("Tebe zachem stolko?");
            this.Quantity += amount;
        }
    }
}
