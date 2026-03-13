using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.ViewModels;

public class InventoryViewModel : INotifyPropertyChanged
{
    private readonly IInventoryService _inventoryService;

    public ObservableCollection<Inventory> Inventories { get; set; } = [];
    
    public InventoryViewModel(IInventoryService invService)
    {
        _inventoryService = invService;
        _ = LoadInventories();
    }

    private async Task LoadInventories()
    {
        var items = await _inventoryService.GetAllInventories();

        foreach (var item in items)
        {
            Inventories.Add(item);
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