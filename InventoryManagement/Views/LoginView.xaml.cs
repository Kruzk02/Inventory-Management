using System.Windows.Controls;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Views;

public partial class LoginView : Page
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}