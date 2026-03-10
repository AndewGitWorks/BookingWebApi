using Domain.Enums;

public class OrderDetailResponse
{
    public Guid Id {get;set;}
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponse> Items { get; set; } = [];
}
public class OrderItemResponse
{
    public Guid ItemId { get; set; }
    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; } = "Undefined product";
    public int Quantity { get; set; }
}