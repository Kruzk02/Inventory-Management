
using System.Net;
using System.Net.Http;
using System.Windows;
using InventoryManagement.Views;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddHttpClient("api", client =>
        {
            client.BaseAddress = new Uri("https://localhost:80");
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
            new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            }
        );
        
        Services = services.BuildServiceProvider();
        
        base.OnStartup(e);
    }
}