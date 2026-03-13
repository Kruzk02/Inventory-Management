using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    Task<Inventory?> GetAllInventories();
}