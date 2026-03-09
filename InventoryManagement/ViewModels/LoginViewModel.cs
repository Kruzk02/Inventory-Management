using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Services;
using InventoryManagement.Views;

namespace InventoryManagement.ViewModels;

public sealed class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthService _authService;
    private readonly AuthState _authState;
    private readonly NavigationService _navigationService;

    public string Username
    {
        get;
        init
        {
            field = value;
            OnPropertyChanged();
        }
    } = null!;

    public ICommand LoginCommand { get; }

    public LoginViewModel(IAuthService authService, AuthState authState, NavigationService navigationService)
    {
        _authService = authService;
        _authState = authState;
        _navigationService = navigationService;
        
        LoginCommand = new AsyncRelayCommand(Login);
    }

    private async Task Login(object? parameter)
    {
        if (parameter is not PasswordBox passwordBox)
            return;

        var response = await _authService.Login(Username, passwordBox.Password);

        var token = response?.Token;
        
        if (token == null)
        {
            MessageBox.Show("Invalid credentials");
            return;
        }

        _authState.SetAccessToken(token);
        MessageBox.Show("Login Successful");
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}