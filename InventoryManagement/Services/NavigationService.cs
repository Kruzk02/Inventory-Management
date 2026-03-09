using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Services;

public class NavigationService(IServiceProvider serviceProvider)
{
    private Frame? _frame;

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public void Navigate<TPage>() where TPage : Page
    {
        var page = serviceProvider.GetRequiredService<TPage>();
        _frame?.Navigate(page);
    }
}