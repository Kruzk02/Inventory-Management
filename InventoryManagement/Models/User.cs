namespace InventoryManagement.Models;

public class User
{
    public string? Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<string> Roles { get; set; }

    public override string ToString()
    {
        return $"User: Id: {Id}, Username: {Username}, Email: {Email}";
    }
}