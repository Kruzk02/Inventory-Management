using InventoryManagement.Models;

namespace InventoryManagement.Dto;

public record InventoryReponse(int Total, List<Inventory> Data);