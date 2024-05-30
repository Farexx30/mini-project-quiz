using QuizWPF.Commands;
using QuizWPF.Services;
using QuizWPF.ViewModels.GenerateQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.SolveQuiz
{
    public class QuizResultsViewModel : ViewModelBase
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

        public RelayCommand NavigateToMainMenuCommand { get; set; } = null!;

        public QuizResultsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToMainMenuCommand = new RelayCommand(NavigateToMainMenu, o => true);
        }

        private void NavigateToMainMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();
    }
}
