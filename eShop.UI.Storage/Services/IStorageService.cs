namespace eShop.UI.Storage.Services;

public interface IStorageService //ett interface som hämtar, sparar och tar bort
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key); //här behövs inget T för att här skickas enbart en nyckel
}
