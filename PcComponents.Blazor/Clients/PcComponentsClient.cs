using PcComponents.Blazor.Models;

namespace PcComponents.Blazor.Clients
{
    public class PcComponentsClient(HttpClient httpClient)
    {
        // Get all components
        public async Task<PcComponent[]> GetComponentsAsync() =>
            await httpClient.GetFromJsonAsync<PcComponent[]>("components") ?? [];

        // Get the component by ID
        public async Task<PcComponent> GetComponentByIdAsync(int id) =>
            await httpClient.GetFromJsonAsync<PcComponent>($"components/{id}")
            ?? throw new Exception("Could not find component");

        // Add a new component
        public async Task AddComponentAsync(PcComponent component)
        {
            var resp = await httpClient.PostAsJsonAsync("components", component);
            resp.EnsureSuccessStatusCode();
        }

        // Update a component
        public async Task UpdateComponentAsync(PcComponent updatedComponent)
        {
            if (updatedComponent is null) throw new ArgumentNullException(nameof(updatedComponent));
            if (updatedComponent.Id <= 0) throw new ArgumentException("Updated component must have a valid Id.");

            var resp = await httpClient.PutAsJsonAsync($"components/{updatedComponent.Id}", updatedComponent);
            resp.EnsureSuccessStatusCode();
        }
    }
}

