using Domain.Enums;

namespace Application.DTOs
{
    public record OrderResponse(Guid Id, DateTime CreatedAt, OrderStatus Status);
}