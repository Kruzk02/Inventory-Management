using InventoryManagement.Models;

namespace InventoryManagement.Dto;

public record InventoriesResponse(int Total, List<Inventory> Data);