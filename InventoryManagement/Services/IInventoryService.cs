using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    Task<InventoriesResponse?> GetAllInventories(string? productName, int? skip, int take);
    Task<bool> Update(int id, InventoryDto inventoryDto);
}