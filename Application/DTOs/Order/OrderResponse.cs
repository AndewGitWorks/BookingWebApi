using Domain.Enums;

namespace Application.DTOs.Order
{
    public record OrderResponse(Guid Id, DateTime CreatedAt, OrderStatus Status);
}