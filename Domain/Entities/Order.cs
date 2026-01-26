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
    }
}
