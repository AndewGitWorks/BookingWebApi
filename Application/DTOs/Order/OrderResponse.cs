using Domain.Enums;

namespace Application.DTOs.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    };
}