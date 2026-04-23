using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryManagement.Models;

namespace InventoryManagement.ViewModels;

public class InventoryDetailViewModel(Inventory inventory) : INotifyPropertyChanged
{
    public string ProductName { get; set; } = inventory.Product.Name;
    public string? CategoryName { get; set; } = inventory.Product.Category.Name;
    public decimal Price { get; set; } = inventory.Product.Price;
    public int Stock { get; set; } = inventory.Stock;

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