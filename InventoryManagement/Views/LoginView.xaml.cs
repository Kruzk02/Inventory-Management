using System.Windows;
using System.Windows.Input;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
}