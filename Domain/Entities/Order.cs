using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public void ChangeStatus(OrderStatus status)
        {
            if(Status == OrderStatus.Cancelled)
            {
                throw new Exception("Cannot change status of a cancelled order.");
            }
            Status = status;
        }
        public void AddItem(Product product, int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity can not be less than 0");
            var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id) ?? throw new Exception("Item not found");
            if(existingItem != null)
                existingItem.Quantity += quantity;
            else
            {
                var item = new OrderItem
                {
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    Quantity = quantity
                };
                Items.Add(item);
            }
        }
    }
}
