using InventoryManagement.Models;

namespace InventoryManagement.Dto;

public record InventoryResponse(int Total, List<Inventory> Data);