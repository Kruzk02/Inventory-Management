using InventoryManagement.Dto;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    Task<InventoryReponse?> GetAllInventories(int skip, int take);
}