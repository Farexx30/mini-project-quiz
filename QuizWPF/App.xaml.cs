using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizWPF.Configurations;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QuizWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = DependencyInjectionConfiguration.CreateHostBuilder().Build();
            DependencyInjectionConfiguration.ServiceProvider = builder.Services;

            var mainWindow = DependencyInjectionConfiguration.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
