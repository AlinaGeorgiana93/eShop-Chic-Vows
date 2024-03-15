using eShop.API.DTO;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;
using eShop.Data.Entities;
using eShop.UI.Http.Clients;
using System.Threading.Tasks;
namespace eShop.UI.Admin;

public class AdminCategoryService : IAdminService
{
    private readonly AdminCategoryHttpClient _categoryAdminClient;

    public List<CategoryGetDTO>? Categories { get; set; }
    public CategoryPutDTO? CategoryToUpdate { get; set; }

    string errorMessage;

    public AdminCategoryService(AdminCategoryHttpClient categoryAdminClient)
    {
        _categoryAdminClient = categoryAdminClient;
    }

    public async Task AddAdminCategory(string categoryName)
    {
        CategoryPostDTO cat = new CategoryPostDTO
        {
            Name = categoryName
        };

        await _categoryAdminClient.AddAdminCategory(cat);
    }

    public async Task DeleteAdminCategory(int catId)
    {
        await _categoryAdminClient.DeleteAdminCategory(catId);
    }

    public async Task AdminGetAllCategories()
    {
        Categories = await _categoryAdminClient.GetAdminCategories();
    }

    public async Task GetAdminCategory(int categoryId)
    {
        Categories = new List<CategoryGetDTO> { await _categoryAdminClient.GetAdminCategory(categoryId) };
    }

    public async Task GetAdminCategoryUpdate(int id)
    {
        CategoryToUpdate = await _categoryAdminClient.GetAdminCategoryToUpdate(id);
    }

    public async Task UpdateAdminCategory(int id, CategoryPutDTO cat)
    {
        await _categoryAdminClient.UpdateAdminCategory(id, cat);
    }
}
