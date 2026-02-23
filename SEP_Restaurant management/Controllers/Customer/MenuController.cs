using Microsoft.AspNetCore.Mvc;
using SEP_Restaurant_management.Services.Interface;
using SEP_Restaurant_management.DTOs.Dish.Response;

namespace SEP_Restaurant_management.Controllers.Customer
{
    [ApiController]
    [Route("api/v1/customers/menu")]
    public class MenuController : ControllerBase
    {
        private readonly ICustomerMenuService _customerMenuService;

        public MenuController(ICustomerMenuService customerMenuService)
        {
            _customerMenuService = customerMenuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetMenu()
        {
            var result = await _customerMenuService.GetMenuAsync();
            return Ok(result);
        }

        [HttpGet("{dishId:int}")]
        public async Task<ActionResult<DishDto>> GetDishDetail(int dishId)
        {
            var dto = await _customerMenuService.GetDishDetailAsync(dishId);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }
    }
}
