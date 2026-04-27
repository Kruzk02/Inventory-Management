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

    public async Task<bool> Update(int id, InventoryDto inventoryDto)
    {
        var client = httpClientFactory.CreateClient("api");
        
        var content = JsonContent.Create(inventoryDto);
        
        var response = await client.PutAsync($"inventory/{id}", content);
        
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        if (response.Content.Headers.ContentLength == 0)
        {
            return true;
        }
        
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}