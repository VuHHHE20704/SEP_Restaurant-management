


namespace SEP_Restaurant_management.ProgramConfig
{
    public static class Tuan
    {
        public static IServiceCollection AddMyServices3(this IServiceCollection services)
        {

            //services.AddScoped<IUnitOfWork>(sp =>
            //{
            //    var dbContext = sp.GetRequiredService<SepDatabaseContext>();
            //    return new UnitOfWork<SepDatabaseContext>(dbContext);
            //});

            //services.AddScoped<ICustomerMenuService, CustomerMenuService>();
            //services.AddScoped<ICustomerCartService, CustomerCartService>();
            //services.AddScoped<ICustomerOrderService, CustomerOrderService>();

            //services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

            return services;
        }
    }
}
