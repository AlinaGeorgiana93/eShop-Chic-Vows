using eShop.UI.Admin;
using eShop.UI.Http.Clients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Microsoft.Extensions.Http;
using eShop.Data.Services;
using AutoMapper;
using eShop.API.DTO;
using eShop.Data.Entities;
using eShop.Data.Services;
using Microsoft.Data.SqlClient;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
 

 builder.Services.AddScoped<IAdminService, AdminCategoryService>();
 builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
 builder.Services.AddSingleton<AdminCategoryService>();
 builder.Services.AddHttpClient<AdminCategoryHttpClient>();

ConfigureAutoMapper(builder.Services);
await builder.Build().RunAsync();

void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<CategoryGetDTO, CategoryPutDTO>().ReverseMap();


    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
    /**********
    ** CORS Cross-Origin Resource Sharing**
    **********/


    builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});
}
