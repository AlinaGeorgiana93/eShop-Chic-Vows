
using Blazored.LocalStorage;

namespace eShop.UI.Storage.Services;

public class LocalStorage(ILocalStorageService localStorage): IStorageService //(ILocalStorageService localStorage) är en konstruktor och ingår i nugget packetet och injeseras för att att komma åt localStorage. LocalStorage sparar data tills man tar bort det manuellt.
{
    public async Task<T?> GetAsync<T>(string key) =>
        await localStorage.GetItemAsync<T>(key); //När get metoden anropas för att hämta något i localstorage används instansen från ovan. Blazords GetItemAsync används som pratar med javascript-bibloteket, som hämtar datat från localstorage i browsern.

    public async Task RemoveAsync(string key) => //tar bort
        await localStorage.RemoveItemAsync(key);

    public async Task SetAsync<T>(string key, T value) => //sparar
        await localStorage.SetItemAsync(key, value);
}
