using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    Task<IEnumerable<Inventory>> GetAllInventories();
}