using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP_Restaurant_management.Services.Interface;
using SEP_Restaurant_management.DTOs.Order.Request;
using SEP_Restaurant_management.DTOs.Order.Response;

namespace SEP_Restaurant_management.Controllers.Customer
{
    [ApiController]
    [Route("api/v1/customers/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public OrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderDetailDto>> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            try
            {
                var response = await _customerOrderService.CreateOrderAsync(request);
                return CreatedAtAction(nameof(GetOrderDetail), new { orderId = response.OrderId }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetOrderHistory(int pageIndex = 1, int pageSize = 20)
        {
            var result = await _customerOrderService.GetOrderHistoryAsync(pageIndex, pageSize);
            return Ok(result.Items);
        }

        [Authorize]
        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrderDetail(int orderId)
        {
            var dto = await _customerOrderService.GetOrderDetailAsync(orderId);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }
    }
}
