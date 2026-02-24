namespace SEP_Restaurant_management.DTOs.Dish.Response;

public sealed class DishDto
{
    public int DishId { get; set; }

    public string DishName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<DishSizeDto> Sizes { get; set; } = new();
}

public sealed class DishSizeDto
{
    public int DishSizeId { get; set; }

    public string DishSizeName { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
