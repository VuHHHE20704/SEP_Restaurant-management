namespace SEP_Restaurant_management.DTOs.Cart.Response;

public sealed class CartItemDto
{
    public int DishSizeId { get; set; }

    public int DishId { get; set; }

    public string DishName { get; set; } = string.Empty;

    public string DishSizeName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string? Note { get; set; }

    public decimal LineTotal { get; set; }
}

public sealed class CartPreviewResponseDto
{
    public List<CartItemDto> Items { get; set; } = new();

    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }
}

