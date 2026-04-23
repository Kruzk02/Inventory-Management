using System.Windows.Input;

namespace InventoryManagement.Commands;

public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute) : ICommand
{
    public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => execute(parameter);
    
    public event EventHandler? CanExecuteChanged;
    
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}