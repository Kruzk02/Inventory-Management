using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.Commands;

public class AsyncRelayCommand(Func<object?, Task> execute) : ICommand
{
    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        try
        {
            await execute(parameter);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public event EventHandler? CanExecuteChanged;
}