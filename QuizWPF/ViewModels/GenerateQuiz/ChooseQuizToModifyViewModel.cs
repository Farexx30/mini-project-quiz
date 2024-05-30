using QuizWPF.Commands;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class ChooseQuizToModifyViewModel : ViewModelBase
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
        public RelayCommand NavigateToQuizDetailsCommand { get; set; } = null!;


        public ChooseQuizToModifyViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToGenerateQuizMenuCommand = new RelayCommand(NavigateToGenerateQuizMenu, o => true);
            NavigateToQuizDetailsCommand = new RelayCommand(NavigateToQuizDetails, o => true);
        }

        private void NavigateToGenerateQuizMenu(object obj) => NavigationService.NavigateTo<GenerateQuizMenuViewModel>();
        private void NavigateToQuizDetails(object obj) => NavigationService.NavigateTo<QuizDetailsViewModel>(Mode.Modify);
    }
}
