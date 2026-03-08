using System.Security;

namespace InventoryManagement.Services;

public class AuthService
{
    public bool Login(string username, string password)
    {
        return username == "admin" && password == "admin";
    }
}