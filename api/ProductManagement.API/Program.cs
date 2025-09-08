using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductManagementDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        providerOptions => providerOptions.EnableRetryOnFailure()
    );
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product Management API",
        Description = "An ASP.NET Core Web API for managing products",
        Contact = new OpenApiContact
        {
            Name = "Ing. Valeria Andrea Guerrero Jaramillo",
            Url = new Uri("https://github.com/ValeriaGJ6/product-management"),
            Email = "valeriaaguerreroj@gmail.com"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.Run();
