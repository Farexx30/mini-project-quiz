using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizWPF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceProvider? ServiceProvider { get; set; }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
               .ConfigureServices((context, services) =>
               {
                   services.RegisterServices();
               });

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<QuizDbContext>(
                option => option.UseSqlServer(ConfigurationManager.ConnectionStrings["mainDatabase"].ConnectionString));

            return services;
        }
    }
}
