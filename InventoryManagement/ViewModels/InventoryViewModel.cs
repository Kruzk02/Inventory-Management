using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Models;
using InventoryManagement.Services;
using InventoryManagement.Views;

namespace InventoryManagement.ViewModels;

public sealed class InventoryViewModel : INotifyPropertyChanged
{
    private readonly IInventoryService _inventoryService;
    private Task Reload() => LoadInventories(_searchQuery, Take, (Page - 1) * Take);
    private string? _searchQuery;
    private CancellationTokenSource? _cts;

    public ObservableCollection<Inventory> Inventories { get; set; } = [];
    public RelayCommand NextCommand { get; }
    public RelayCommand PreviousCommand { get; }
    public ICommand OpenDetailsCommand { get; }

    public Inventory? SelectedInventory
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public int Page
    {
        get;
        set
        {
            if (!SetField(ref field, value)) return;

            OnPropertyChanged(nameof(PageDisplay));

            NextCommand.RaiseCanExecuteChanged();
            PreviousCommand.RaiseCanExecuteChanged();
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
        get;
        set => SetField(ref field, value);
    }

    public int TotalPages
    {
        get;
        set
        {
            if (SetField(ref field, value))
            {
                OnPropertyChanged(nameof(PageDisplay));
            }
        }
    }

    public string PageDisplay => $"{Page} / {TotalPages}";

    public bool IsLoading
    {
        get;
        set => SetField(ref field, value);
    }

    public InventoryViewModel(IInventoryService invService)
    {
        _inventoryService = invService;

        NextCommand = new RelayCommand(_ => NextPage(), _ => Page < TotalPages);
        PreviousCommand = new RelayCommand(_ => PreviousPage(), _ => Page > 1);
        OpenDetailsCommand = new RelayCommand(_ => OpenDetails(), _ => SelectedInventory != null);

        _ = Reload();
    }

    private void OpenDetails()
    {
        if (SelectedInventory != null)
        {
            var vm = new InventoryDetailViewModel(SelectedInventory, _inventoryService);

            var window = new InventoryDetailView
            {
                DataContext = vm
            };

            window.ShowDialog();
        }

        _ = Reload();
    }

    private void NextPage()
    {
        if (Page >= TotalPages) return;
        Page++;
        _ = Reload();
    }

    private void PreviousPage()
    {
        if (Page <= 1) return;
        Page--;
        _ = Reload();
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

                TotalUnits = items.Total;
                TotalPages = (items.Total + take - 1) / take;

                NextCommand.RaiseCanExecuteChanged();
                PreviousCommand.RaiseCanExecuteChanged();
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
            await Reload();
        }
        catch (TaskCanceledException)
        {
        }
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