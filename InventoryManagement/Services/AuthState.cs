namespace InventoryManagement.Services;

public class AuthState
{
    public string? AccessToken { get; private set; }

    public void SetAccessToken(string token)
    {
        AccessToken = token;
    }

    public void Clear()
    {
        AccessToken = null;
    }
}