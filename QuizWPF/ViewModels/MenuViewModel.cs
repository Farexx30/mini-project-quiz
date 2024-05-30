using Microsoft.EntityFrameworkCore;
using QuizWPF.Commands;
using QuizWPF.Models;
using QuizWPF.Services;
using QuizWPF.ViewModels.GenerateQuiz;
using QuizWPF.ViewModels.SolveQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizWPF.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToGenerateQuizMenuCommand { get; set; } = null!;
        public RelayCommand NavigateToQuizListToSolveCommand { get; set; } = null!;


        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToGenerateQuizMenuCommand = new RelayCommand(NavigateToGenerateQuizMenuViewModel, o => true);
            NavigateToQuizListToSolveCommand = new RelayCommand(NavigateToQuizListToSolveViewModel, o => true);
        }

        private void NavigateToGenerateQuizMenuViewModel(object obj) => NavigationService.NavigateTo<GenerateQuizMenuViewModel>();
        private void NavigateToQuizListToSolveViewModel(object obj) => NavigationService.NavigateTo<QuizListToSolveViewModel>();
    }
}
