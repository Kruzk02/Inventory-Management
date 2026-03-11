using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IAuthService
{
    Task<LoginResponse?> Login(string username, string password);
    Task<User?> GetCurrentUserInfo(string accessToken);
}