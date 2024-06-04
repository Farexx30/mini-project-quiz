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
        private readonly ISharedSolvedQuizDataService _sharedSolvedQuizDataService;

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

        //Bindings:
        private string _solvedQuizResults = null!;
        public string SolvedQuizResults
        {
            get => _solvedQuizResults;
            set
            {
                _solvedQuizResults = value;
                OnPropertyChanged(nameof(SolvedQuizResults));
            }
        }

        public RelayCommand NavigateToMainMenuCommand { get; set; } = null!;

        public QuizResultsViewModel(INavigationService navigationService, ISharedSolvedQuizDataService sharedSolvedQuizDataService)
        {
            _navigationService = navigationService;
            _sharedSolvedQuizDataService = sharedSolvedQuizDataService;    

            NavigateToMainMenuCommand = new RelayCommand(NavigateToMainMenu, _ => true);

            Initialize();
        }

        private void Initialize()
        {
            SolvedQuizResults = $"Uzyskane punkty: {_sharedSolvedQuizDataService.Score}" +
                $"/{_sharedSolvedQuizDataService.MaxScore}" +
                $"{Environment.NewLine}Czas: {_sharedSolvedQuizDataService.TimeElapsed}";
        }

        private void NavigateToMainMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();
    }
}
