using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEP_Restaurant_management.DTOs.Dish.Response;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.Repositories.Interface;
using SEP_Restaurant_management.Services.Interface;

namespace SEP_Restaurant_management.Services.Implementation;

public class CustomerMenuService : BaseService<CustomerMenuService>, ICustomerMenuService
{
    public CustomerMenuService(IUnitOfWork unitOfWork, ILogger<CustomerMenuService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IReadOnlyList<DishDto>> GetMenuAsync()
    {
        var dishRepo = _unitOfWork.GetRepository<Dish>();
        var dishes = await dishRepo.GetListAsync(
            predicate: d => d.IsActive == true,
            include: q => q.Include(d => d.Category)
                           .Include(d => d.DishSizes)
                               .ThenInclude(ds => ds.Price)
        );

        return _mapper.Map<List<DishDto>>(dishes);
    }

    public async Task<DishDto?> GetDishDetailAsync(int dishId)
    {
        var dishRepo = _unitOfWork.GetRepository<Dish>();
        var dish = await dishRepo.SingleOrDefaultAsync(
            predicate: d => d.DishId == dishId && d.IsActive == true,
            include: q => q.Include(d => d.Category)
                           .Include(d => d.DishSizes)
                               .ThenInclude(ds => ds.Price)
        );

        if (dish == null) return null;

        return _mapper.Map<DishDto>(dish);
    }
}
