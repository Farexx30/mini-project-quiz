using QuizWPF.Commands;
using QuizWPF.Services;
using QuizWPF.ViewModels.GenerateQuiz;
using QuizWPF.Views.SolveQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.SolveQuiz
{
    public class QuizListToSolveViewModel : ViewModelBase
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
        public RelayCommand NavigateToSelectedQuizCommand { get; set; } = null!;


        public QuizListToSolveViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToMainMenuCommand = new RelayCommand(NavigateToMainMenu, o => true);
            NavigateToSelectedQuizCommand = new RelayCommand(NavigateToSelectedQuiz, o => true);
        }

        private void NavigateToMainMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();
        private void NavigateToSelectedQuiz(object obj) => NavigationService.NavigateTo<QuestionSolveViewModel>(); //Tutaj wypadałoby sprawdzić najpierw, czy w ogole jakis quiz jest wybrany.
    }
}
