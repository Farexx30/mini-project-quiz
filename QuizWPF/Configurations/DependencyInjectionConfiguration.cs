using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizWPF.Models;
using QuizWPF.Services;
using QuizWPF.ViewModels;
using QuizWPF.ViewModels.GenerateQuiz;
using QuizWPF.ViewModels.SolveQuiz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Configurations
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
            //Register DbContext:
            services.AddDbContext<QuizDbContext>(
                options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["mainDatabase"].ConnectionString));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Register services:
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<ISharedQuizDataService, SharedQuizDataService>();
            services.AddScoped<ISharedSolvedQuizDataService, SharedQuizDataService>();
            services.AddScoped<IQuizRepositoryService, QuizRepositoryService>();

            //Register ViewModels with navigation Func
            services.AddSingleton<Func<Type, ViewModelBase>>(provider =>
                viewModelType => (ViewModelBase)provider.GetRequiredService(viewModelType));

            services.AddSingleton(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddTransient<MainViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<QuizListToSolveViewModel>();
            services.AddTransient<QuestionSolveViewModel>();
            services.AddTransient<QuizResultsViewModel>();
            services.AddTransient<ChooseQuizToModifyViewModel>();
            services.AddTransient<GenerateQuizMenuViewModel>();
            services.AddTransient<ModifyQuestionViewModel>();
            services.AddTransient<QuizQuestionsListViewModel>();
            services.AddTransient<QuizConfirmationViewModel>();
            services.AddTransient<QuizDetailsViewModel>();

            return services;
        }
    }
}
