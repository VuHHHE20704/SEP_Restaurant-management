using SEP_Restaurant_management.DTOs.Dish.Response;

namespace SEP_Restaurant_management.Services.Interface;

public interface ICustomerMenuService
{
    Task<IReadOnlyList<DishDto>> GetMenuAsync();

    Task<DishDto?> GetDishDetailAsync(int dishId);
}
