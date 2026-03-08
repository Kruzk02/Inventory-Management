using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InventoryManagement.Commands;
using InventoryManagement.Services;

namespace InventoryManagement.ViewModels;

public sealed class LoginViewModel : INotifyPropertyChanged
{
    private string _username = null!;

    private readonly AuthService _authService = new();

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(Login);
    }

    private void Login(object? parameter)
    {
        if (parameter is not PasswordBox passwordBox)
            return;

        var success = _authService.Login(Username, passwordBox.Password);

        MessageBox.Show(success ? "Login successful" : "Invalid credentials");
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}