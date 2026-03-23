using System.Collections.ObjectModel;
using System.Windows.Controls;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Views;

public partial class InventoryView : Page
{
    public InventoryView(InventoryViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}