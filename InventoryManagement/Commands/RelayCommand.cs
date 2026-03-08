using System.Windows.Input;

namespace InventoryManagement.Commands;

public class RelayCommand(Action<object?> execute) : ICommand
{
    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        execute(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}