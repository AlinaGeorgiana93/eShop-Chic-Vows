using AutoMapper;
using eShop.API.DTO;
using eShop.API.DTO.DTOs;
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
    //app.AddEndpoint<CategoryFilter, CategoryFilterPostDTO>();
    app.AddEndpoint<Filter,FilterTypePostDTO, FilterTypePutDTO, FilterTypeGetDTO > ();
    /*app.MapGet($"/api/productsbycategory/{{categoryId}}", async (IDbService db, int categoryId) =>
    {
        try
        {
            return Results.Ok(await ((FilterDbService)db).GetCategoriesWithAllRelatedDataAsync());
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't get the requested products of type {typeof(Filter).Name}.");
    });*/

}

void RegisterServices()
{
    ConfigureAutoMapper();
    builder.Services.AddScoped<IDbService, CategoryDbService>();
}

void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Filter, FilterTypePostDTO>().ReverseMap();
        cfg.CreateMap<Filter, FilterTypePutDTO>().ReverseMap();
        cfg.CreateMap<Filter, FilterTypeGetDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterPostDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterDeleteDTO>().ReverseMap();
        //cfg.CreateMap<Size, OptionDTO>().ReverseMap();
        //cfg.CreateMap<Color, OptionDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
