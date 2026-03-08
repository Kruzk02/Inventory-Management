
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
        .ConfigurePrimaryHttpMessageHandler(() =>
            new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            }
        );

        services.AddSingleton<IAuthService, AuthService>();
        
        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginView>();
        
        Services = services.BuildServiceProvider();
        
        var view = Services.GetRequiredService<LoginView>();
        view.Show();
        
        base.OnStartup(e);
    }
}