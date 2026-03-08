using InventoryManagement.Dto;

namespace InventoryManagement.Services;

public interface IAuthService
{
    Task<LoginResponse?> Login(string username, string password);
}