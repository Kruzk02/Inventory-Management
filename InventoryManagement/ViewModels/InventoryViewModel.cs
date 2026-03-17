using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.ViewModels;

public class InventoryViewModel : INotifyPropertyChanged
{
    private readonly IInventoryService _inventoryService;

    public ObservableCollection<Inventory> Inventories { get; set; } = [];
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }

    public int Page
    {
        get;
        set => SetField(ref field, value);
    } = 1;

    public int Take { get; set; } = 50;
    
    public InventoryViewModel(IInventoryService invService)
    {
        _inventoryService = invService;

        NextCommand = new RelayCommand(NextPage);
        PreviousCommand = new RelayCommand(PreviousPage);
        
        _ = LoadInventories(Take, (Page - 1) * Take);
    }

    private void NextPage(object? parameter)
    {
        Page++;
        _ = LoadInventories(Take, (Page - 1) * Take);
    }

    private void PreviousPage(object? parameter)
    {
        if (Page > 1)
        {
            Page--;
            _ = LoadInventories(Take, (Page - 1) * Take);    
        }
    }

    private async Task LoadInventories(int take, int skip)
    {
        Inventories.Clear();
        
        var items = await _inventoryService.GetAllInventories(skip, take);

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