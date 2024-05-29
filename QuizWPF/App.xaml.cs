using Microsoft.Extensions.Configuration;
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
        public App()
        {
            var builder = DependencyInjectionConfiguration.CreateHostBuilder().Build();
            DependencyInjectionConfiguration.ServiceProvider = builder.Services;
        }
    }

}
