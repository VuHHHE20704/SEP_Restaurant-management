namespace SEP_Restaurant_management.DTOs.Order.Response;

public sealed class OrderSummaryDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string? OrderCode { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }
}

public sealed class OrderDetailDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string? OrderCode { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Note { get; set; }

    public decimal? TotalPrice { get; set; }

    public List<OrderLineDto> Items { get; set; } = new();
}

public sealed class OrderLineDto
{
    public int DishSizeId { get; set; }

    public int DishId { get; set; }

    public string DishName { get; set; } = string.Empty;

    public string DishSizeName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal { get; set; }

    public string? Note { get; set; }
}

