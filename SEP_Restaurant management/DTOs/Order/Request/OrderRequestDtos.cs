namespace SEP_Restaurant_management.DTOs.Order.Request;

public sealed class OrderItemRequestDto
{
    public int DishSizeId { get; set; }

    public int Quantity { get; set; }

    public string? Note { get; set; }
}

public sealed class CreateOrderRequestDto
{
    public DateTime? ArrivalTime { get; set; }

    public string? Note { get; set; }

    public List<OrderItemRequestDto> Items { get; set; } = new();
}

