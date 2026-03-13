namespace InventoryManagement.Models;

public class Inventory
{
    public int Id { get; set; }
    public required Product Product { get; set; }
    public int Stock { get; set; }
    public DateTime UpdatedAt { get; set; }
}