using eShop.API.DTO;

namespace eShop.UI.Admin
{
    public interface IAdminService
    {
        List<CategoryGetDTO>? Categories { get; set; }
        CategoryPutDTO? CategoryToUpdate { get; set; }

        Task AddAdminCategory(string categoryName);
        Task DeleteAdminCategory(int catId);
        Task AdminGetAllCategories();
        Task GetAdminCategory(int categoryId);
        Task GetAdminCategoryUpdate(int id);
        Task UpdateAdminCategory(int id, CategoryPutDTO cat);
    }
}
