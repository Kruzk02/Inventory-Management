using System.Windows;
using InventoryManagement.Services;

namespace InventoryManagement.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(NavigationService navigation)
    {
        InitializeComponent();
        navigation.SetFrame(MainFrame);
        navigation.Navigate<LoginView>();
    }
}