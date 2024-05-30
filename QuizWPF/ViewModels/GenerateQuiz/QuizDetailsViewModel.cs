using QuizWPF.Commands;
using QuizWPF.Services;
using QuizWPF.ViewModels.SolveQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class QuizDetailsViewModel : ViewModelBase
    {
        private Mode _mode;
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

        public RelayCommand NavigateToPreviousCommand { get; set; } = null!;
        public RelayCommand NavigateToQuestionListCommand { get; set; } = null!;


        public QuizDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToPreviousCommand = new RelayCommand(NavigateToPrevious, o => true);
            NavigateToQuestionListCommand = new RelayCommand(NavigateToQuestionList, o => true);
        }
        public void SetMode(Mode mode) => _mode = mode;

        private void NavigateToPrevious(object obj)
        {
            if (_mode == Mode.Add) NavigationService.NavigateTo<GenerateQuizMenuViewModel>();
            else NavigationService.NavigateTo<ChooseQuizToModifyViewModel>();
        }
        private void NavigateToQuestionList(object obj) => NavigationService.NavigateTo<QuizQuestionsListViewModel>(_mode);
    }
}
