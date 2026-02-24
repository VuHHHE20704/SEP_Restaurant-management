using AutoMapper;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.DTOs.Cart.Response;
using SEP_Restaurant_management.DTOs.Dish.Response;
using SEP_Restaurant_management.DTOs.Order.Response;

namespace SEP_Restaurant_management.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Dish, DishDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.DishSizes.Where(ds => ds.IsActive == true)));

        CreateMap<DishSize, DishSizeDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.PriceValue));

        CreateMap<DishSize, CartItemDto>()
            .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.DishName))
            .ForMember(dest => dest.DishSizeName, opt => opt.MapFrom(src => src.DishSizeName))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price.PriceValue))
            .ForMember(dest => dest.LineTotal, opt => opt.Ignore())
            .ForMember(dest => dest.Quantity, opt => opt.Ignore())
            .ForMember(dest => dest.Note, opt => opt.Ignore());

        CreateMap<Models.Order, OrderSummaryDto>();

        CreateMap<Models.Order, OrderDetailDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails));

        CreateMap<Models.OrderDetail, OrderLineDto>()
            .ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.DishSize.DishId))
            .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.DishSize.Dish.DishName))
            .ForMember(dest => dest.DishSizeName, opt => opt.MapFrom(src => src.DishSize.DishSizeName))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice ?? 0))
            .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => (src.UnitPrice ?? 0) * src.Quantity));
    }
}
