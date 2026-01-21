using PcComponents.Blazor.Models;

namespace PcComponents.Blazor.Clients
{
    public class CategoriesClient(HttpClient httpClient)
    {
        public async Task<Category[]> GetCategoriesAsync() =>
            await httpClient.GetFromJsonAsync<Category[]>("/categories") ?? [];
    }
}
