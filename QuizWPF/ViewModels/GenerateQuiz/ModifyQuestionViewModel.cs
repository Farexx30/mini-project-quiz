using QuizWPF.Commands;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class ModifyQuestionViewModel : ViewModelBase
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

        public RelayCommand NavigateToQuizQuestionsListCommand { get; set; } = null!;

        public ModifyQuestionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToQuizQuestionsListCommand = new RelayCommand(NavigateToQuizQuestionsList, o => true);
        }
        
        public void SetMode(Mode mode) => _mode = mode;

        private void NavigateToQuizQuestionsList(object obj)
        {
            //jakas tam logika
            NavigationService.NavigateTo<QuizQuestionsListViewModel>(_mode);
        }

    }
}
