using System.Net.Http;
using System.Net.Http.Json;
using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    public async Task<LoginResponse?> Login(string username, string password)
    {
        var client = httpClientFactory.CreateClient("api");
        
        var request = new LoginRequest(username, password);
        var response = await client.PostAsJsonAsync("user/login", request);

        if (!response.IsSuccessStatusCode)
            return null;
        
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<User?> GetCurrentUserInfo(string username)
    {
        var client = httpClientFactory.CreateClient("api");
        var response = await client.GetAsync($"user");

        if (!response.IsSuccessStatusCode)
            return null;
        
        return await response.Content.ReadFromJsonAsync<User>();
    }
}