using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.ViewModels;

public sealed class InventoryViewModel : INotifyPropertyChanged
{
    private readonly IInventoryService _inventoryService;
    private int _totalData;
    private int _totalPages;
    private string? _searchQuery;
    private bool _isLoading;
    private CancellationTokenSource? _cts;

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

    public string? SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (!SetField(ref _searchQuery, value)) return;
            
            Page = 1;

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            _ = DebouncedSearch(_cts.Token);
        }
    }

    public int TotalUnits
    {
        get => _totalData;
        set => SetField(ref _totalData, value);
    }

    public int TotalPages
    {
        get => _totalPages;
        set => SetField(ref _totalPages, value);
    }

    public string PageDisplay => $"{Page} / {TotalPages}";
    
    public bool IsLoading
    {
        get => _isLoading;
        set => SetField(ref _isLoading, value);
    }
    
    public InventoryViewModel(IInventoryService invService)
    {
        _inventoryService = invService;

        NextCommand = new RelayCommand(NextPage);
        PreviousCommand = new RelayCommand(PreviousPage);
        
        _ = LoadInventories(_searchQuery, Take, (Page - 1) * Take);
    }

    private void NextPage(object? parameter)
    {
        if (Page >= TotalPages) return;
        Page++;
        _ = LoadInventories(_searchQuery, Take, (Page - 1) * Take);
    }

    private void PreviousPage(object? parameter)
    {
        if (Page <= 1) return;
        Page--;
        _ = LoadInventories(_searchQuery, Take, (Page - 1) * Take);
    }

    private async Task LoadInventories(string? productName, int take, int skip)
    {
        try
        {
            IsLoading = true;
            
            var minDelay = Task.Delay(300);
            var items = await _inventoryService.GetAllInventories(productName, skip, take);

            await minDelay;
            if (items != null)
            {
                Inventories = new ObservableCollection<Inventory>(items.Data);
                OnPropertyChanged(nameof(Inventories));

                SetField(ref _totalData, items.Total);
                SetField(ref _totalPages, (_totalData + take - 1) / take);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private async Task DebouncedSearch(CancellationToken token)
    {
        try
        {
            await Task.Delay(500, token);
            await LoadInventories(_searchQuery, Take, (Page - 1) * Take);
        }
        catch (TaskCanceledException) { }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}