using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class ChooseQuizToModifyViewModel : ViewModelBase
    {
        private readonly IQuizRepositoryService _quizRepositoryService;
        private readonly ISharedQuizDataService _sharedQuizDataService;

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
        private ObservableCollection<QuizDto> _allQuizzes = [];
        public ObservableCollection<QuizDto> AllQuizzes
        {
            get => _allQuizzes;
            set
            {
                _allQuizzes = value;
                OnPropertyChanged(nameof(AllQuizzes));
            }
        }

        private QuizDto? _selectedQuiz = null;
        public QuizDto? SelectedQuiz
        {
            get => _selectedQuiz;
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }


        public RelayCommand NavigateToGenerateQuizMenuCommand { get; set; } = null!;
        public RelayCommand NavigateToQuizDetailsCommand { get; set; } = null!;


        public ChooseQuizToModifyViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService, IQuizRepositoryService quizRepositoryService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;
            _quizRepositoryService = quizRepositoryService;
            
            NavigateToGenerateQuizMenuCommand = new RelayCommand(NavigateToGenerateQuizMenu, o => true);
            NavigateToQuizDetailsCommand = new RelayCommand(NavigateToQuizDetails, o => true);

            Initialize();
        }

        private void Initialize()
        {
            AllQuizzes = new(_quizRepositoryService.GetAllQuizzes()); 
        }

        private void NavigateToGenerateQuizMenu(object obj) => NavigationService.NavigateTo<GenerateQuizMenuViewModel>();
        private void NavigateToQuizDetails(object obj)
        {
            if (SelectedQuiz is not null)
            {                
                _sharedQuizDataService.CurrentQuizDto = SelectedQuiz;
                _sharedQuizDataService.CurrentQuizDto.Questions = _quizRepositoryService.GetQuizData(SelectedQuiz.Id);

                NavigationService.NavigateTo<QuizDetailsViewModel>(Mode.Modify);
            }    
        }
    }
}
