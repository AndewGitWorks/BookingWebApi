using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public record OrdersListResponse
    (
        Guid Id,
        OrderStatus Status,
        decimal TotalAmount,
        DateTime CreatedAt,
        List<OrderItemResponse> Items
    );
}
