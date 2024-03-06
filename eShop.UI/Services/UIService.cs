using eShop.UI.Storage.Services;
using System.ComponentModel;

namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp,
    ProductHttpClient productHttp, IMapper mapper, IStorageService storage)
{
    public bool HasSizeOrColor { get; set; } = true;
    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<CartItemDTO> CartItems { get; set; } = []; //CartItemDTO används istället för productGetDTO för att vi vill inte ha listorna för färg och storlek. Vi vill spara undan som CartItem som har en färg och storlek som vi vallt.
    public List<LinkGroup> CaregoryLinkGroups { get; private set; } =
    [
        new LinkGroup {
            Name = "Categories"
            /*,LinkOptions = new(){
                new LinkOption { Id = 1, Name = "Women", IsSelected = true },
                new LinkOption { Id = 2, Name = "Men", IsSelected = false },
                new LinkOption { Id = 3, Name = "Children", IsSelected = false }
            }*/
        }
    ];
    public int CurrentCategoryId { get; set; }

    public async Task GetLinkGroup()
    {
        Categories = await categoryHttp.GetCategoriesAsync();
        CaregoryLinkGroups[0].LinkOptions = mapper.Map<List<LinkOption>>(Categories);
        var linkOption = CaregoryLinkGroups[0].LinkOptions.FirstOrDefault();
        linkOption!.IsSelected = true;
    }

    public async Task OnCategoryLinkClick(int id)
    {
        CurrentCategoryId = id;
        await GetProductsAsync();
        //Products.ForEach(p => p.Colors!.First().IsSelected = true);

        CaregoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CaregoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(CurrentCategoryId)).IsSelected = true;
    }

    public async Task GetProductsAsync() =>
        Products = await productHttp.GetProductsAsync(CurrentCategoryId);

    public async Task<T> ReadStorage<T>(string key)// where T : class , Task<List<T>>
    {
        //if (string.IsNullOrEmpty(key) || storage is null) return new T();
        return await storage.GetAsync<T>(key);
    }
    public async Task<T> ReadSingleStorage<T>(string key)// where T : class , Returnerar en produkt
    {
        return await storage.GetAsync<T>(key);
    }

    public async Task SaveToStorage<T>(string key, T value)// where T : class, Sparar undan nyckel och värdet
    {
        if (string.IsNullOrEmpty(key) || storage is null) return;
        await storage.SetAsync<T>(key, value); //SetAsync tar in nyckeln och värdet
    }
    public async Task RemoveFromStorage(string key)// where T : class , tar bort via nyckeln
    {
        if (string.IsNullOrEmpty(key) || storage is null) return;
        await storage.RemoveAsync(key);
    }


}