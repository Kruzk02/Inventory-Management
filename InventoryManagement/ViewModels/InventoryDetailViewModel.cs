using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Dto;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.ViewModels;

public class InventoryDetailViewModel : INotifyPropertyChanged
{
    public string ProductName { get; set; }
    public string? CategoryName { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    public ICommand SaveCommand { get; }

    private Inventory _inventory;
    private IInventoryService _inventoryService;
    
    public InventoryDetailViewModel(Inventory inventory, IInventoryService inventoryService)
    {
        ProductName = inventory.Product.Name;
        CategoryName = inventory.Product.Category.Name;
        Price = inventory.Product.Price;
        Stock = inventory.Stock;
        
        _inventory = inventory;

        SaveCommand = new AsyncRelayCommand(Save);
        
        _inventoryService = inventoryService;
    }

    private async Task Save(object? parameter)
    {
        if (Stock != _inventory.Stock)
        {
            var inventoryDto = new InventoryDto(_inventory.Product.Id, Stock);
            var isUpdated = await _inventoryService.Update(_inventory.Id, inventoryDto);
            if (isUpdated)
            {
                MessageBox.Show("Data update successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}