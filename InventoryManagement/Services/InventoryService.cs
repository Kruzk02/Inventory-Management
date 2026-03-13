using System.Net.Http;
using System.Net.Http.Json;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class InventoryService(IHttpClientFactory httpClientFactory) : IInventoryService
{
    public async Task<IEnumerable<Inventory>> GetAllInventories()
    {
        var client = httpClientFactory.CreateClient("api");

        var response = await client.GetAsync("inventory");
        
        return (await response.Content.ReadFromJsonAsync<IEnumerable<Inventory>>()) ?? [];
    }
}