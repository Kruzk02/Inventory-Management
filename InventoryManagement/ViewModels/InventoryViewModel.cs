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
    private int _totalData;
    private int _totalPages;

    public ObservableCollection<Inventory> Inventories { get; set; } = [];
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }

    public int Page
    {
        get;
        set
        {
            SetField(ref field, value);
            OnPropertyChanged(nameof(PageDisplay));    
        }
    } = 1;

    public int Take { get; set; } = 50;
    
    public int TotalUnits
    {
        get => _totalData;
        set => SetField(ref _totalData, value);
    }

    public int TotalPages
    {
        get => _totalPages;
        set => SetField(ref _totalData, value);
    }

    public string PageDisplay => $"{Page} / {TotalPages}";
    
    public InventoryViewModel(IInventoryService invService)
    {
        _inventoryService = invService;

        NextCommand = new RelayCommand(NextPage);
        PreviousCommand = new RelayCommand(PreviousPage);
        
        _ = LoadInventories(Take, (Page - 1) * Take);
    }

    private void NextPage(object? parameter)
    {
        if (Page >= TotalPages) return;
        Page++;
        _ = LoadInventories(Take, (Page - 1) * Take);
    }

    private void PreviousPage(object? parameter)
    {
        if (Page <= 1) return;
        Page--;
        _ = LoadInventories(Take, (Page - 1) * Take);
    }

    private async Task LoadInventories(int take, int skip)
    {
        Inventories.Clear();
        
        var items = await _inventoryService.GetAllInventories(skip, take);

        if (items != null)
        {
            foreach (var item in items.Data)
            {
                Inventories.Add(item);
            }

            _totalData = items.Total;
            _totalPages = _totalData / take + 1;
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