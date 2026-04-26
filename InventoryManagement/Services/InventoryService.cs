using System.Net.Http;
using System.Net.Http.Json;
using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class InventoryService(IHttpClientFactory httpClientFactory) : IInventoryService
{
    public async Task<InventoriesResponse?> GetAllInventories(string? productName, int? skip, int take)
    {
        var client = httpClientFactory.CreateClient("api");

        var response = await client.GetAsync($"inventory?productName={productName}&skip={skip}&take={take}");
        
        return await response.Content.ReadFromJsonAsync<InventoriesResponse>();
    }
}