using System.Net.Http;
using System.Net.Http.Json;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class InventoryService(IHttpClientFactory httpClientFactory) : IInventoryService
{
    public async Task<IEnumerable<Inventory>> GetAllInventories(int skip, int take)
    {
        var client = httpClientFactory.CreateClient("api");

        var response = await client.GetAsync($"inventory?skip={skip}&take={take}");
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<Inventory>>() ?? [];
    }
}