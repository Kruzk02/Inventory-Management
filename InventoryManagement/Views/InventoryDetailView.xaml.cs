using System.Windows;

namespace InventoryManagement.Views;

public partial class InventoryDetailView : Window
{
    public InventoryDetailView()
    {
        InitializeComponent();
    }

    private void Cancel_click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}