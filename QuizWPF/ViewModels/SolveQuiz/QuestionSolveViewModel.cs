using QuizWPF.Commands;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizWPF.ViewModels.SolveQuiz
{
    public class QuestionSolveViewModel : ViewModelBase
    {
        private bool _isQuizStarted = false;
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

        public RelayCommand StartQuizCommand { get; set; } = null!;
        public RelayCommand EndQuizCommand { get; set; } = null!;
        public RelayCommand GoToNextQuestionCommand { get; set; } = null!;


        public QuestionSolveViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartQuizCommand = new RelayCommand(StartQuiz, CanExecuteStartQuizCommand);
            EndQuizCommand = new RelayCommand(EndQuiz, CanExecuteEndQuizCommand);
            GoToNextQuestionCommand = new RelayCommand(GoToNextQuestion, CanExecuteGoToNextQuestionCommand);
        }

        private void StartQuiz(object obj)
        {
            MessageBox.Show("Start");
            _isQuizStarted = true;
        }

        private void EndQuiz(object obj)
        {
            MessageBox.Show("Koniec");
            NavigationService.NavigateTo<QuizResultsViewModel>();
        }

        private void GoToNextQuestion(object obj)
        {
            MessageBox.Show("Następne pytanie");
        }


        //CanExecute:
        private bool CanExecuteStartQuizCommand(object obj) => !_isQuizStarted;
        private bool CanExecuteEndQuizCommand(object obj) => _isQuizStarted;
        private bool CanExecuteGoToNextQuestionCommand(object obj) => _isQuizStarted;
    }
}
