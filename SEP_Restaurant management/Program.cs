using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.ProgramConfig;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SepDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"));
});
builder.Services.AddMyServices1();
builder.Services.AddMyServices2();
builder.Services.AddMyServices3();
builder.Services.AddMyServices4();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Identity
builder.Services.AddIdentity<UserIdentity, IdentityRole>()
    .AddEntityFrameworkStores<SepDatabaseContext>()
    .AddDefaultTokenProviders();

// Add Authentication + Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
