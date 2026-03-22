using System.Net.Http;
using System.Net.Http.Json;
using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class InventoryService(IHttpClientFactory httpClientFactory) : IInventoryService
{
    public async Task<InventoryResponse?> GetAllInventories(int skip, int take)
    {
        var client = httpClientFactory.CreateClient("api");

        var response = await client.GetAsync($"inventory?skip={skip}&take={take}");
        
        return await response.Content.ReadFromJsonAsync<InventoryResponse>();
    }
}