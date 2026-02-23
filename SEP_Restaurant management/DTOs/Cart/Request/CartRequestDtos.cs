namespace SEP_Restaurant_management.DTOs.Cart.Request;

using SEP_Restaurant_management.DTOs.Order.Request;

public sealed class CartPreviewRequestDto
{
    public List<OrderItemRequestDto> Items { get; set; } = new();
}

