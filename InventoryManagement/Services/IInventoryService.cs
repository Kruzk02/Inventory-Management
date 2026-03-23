using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    Task<InventoryResponse?> GetAllInventories(string? productName, int? skip, int take);
}