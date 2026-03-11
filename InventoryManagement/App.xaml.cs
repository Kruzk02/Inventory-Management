
using System.Net;
using System.Net.Http;
using System.Windows;
using InventoryManagement.Services;
using InventoryManagement.ViewModels;
using InventoryManagement.Views;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IServiceProvider? Services { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddHttpClient("api", client =>
        {
            client.BaseAddress = new Uri("http://localhost:80/");
        })
        .AddHttpMessageHandler<AuthHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
            new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            }
        );

        ConfigureServices(services);
        
        Services = services.BuildServiceProvider();
        
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        
        base.OnStartup(e);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<NavigationService>();
        services.AddSingleton<AuthState>();
        services.AddSingleton<AuthHandler>();
        services.AddSingleton<IAuthService, AuthService>();
        
        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginView>();
        services.AddTransient<InventoryView>();
        services.AddTransient<MainWindow>();
    }
}