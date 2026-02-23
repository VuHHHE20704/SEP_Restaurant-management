using SEP_Restaurant_management.DTOs.Cart.Request;
using SEP_Restaurant_management.DTOs.Cart.Response;

namespace SEP_Restaurant_management.Services.Interface;

public interface ICustomerCartService
{
    Task<CartPreviewResponseDto> PreviewCartAsync(CartPreviewRequestDto request);
}
