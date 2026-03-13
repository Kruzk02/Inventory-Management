using System.Windows.Controls;
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