using AutoMapper;
using eShop.API.DTO;
using eShop.API.Extensions.Extensions;
using eShop.Data.Contexts;
using eShop.Data.Entities;
using eShop.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// SQL Server Service Registration
builder.Services.AddDbContext<EShopContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("EShopConnection")));

/**********
 ** CORS Cross-Origin Resource Sharing**
 **********/
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
    );
});

RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

RegisterEndpoints();

/************************
 ** CORS Configuration **
 ************************/
app.UseCors("CorsAllAccessPolicy");

app.Run();

void RegisterEndpoints()
{
    app.AddEndpoint<Product, ProductPostDTO, ProductPutDTO, ProductGetDTO>();//+ "{categoryId}"
    app.MapGet($"/api/productsbycategory/{{categoryId}}" , async (IDbService db, int categoryId) =>
    {
        try
        {
            var result = await ((ProductDbService)db).GetProductsByCategoryAsync(categoryId);
            return Results.Ok(result);
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't get the requested products of type {typeof(Product).Name}.");
    });
}

void RegisterServices()
{
    ConfigureAutoMapper();
    builder.Services.AddScoped<IDbService, ProductDbService>();
}

void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Product, ProductPostDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductPutDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductGetDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePostDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePutDTO>().ReverseMap();
        cfg.CreateMap<Size, SizeGetDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPostDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPutDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorGetDTO>().ReverseMap();
        cfg.CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}