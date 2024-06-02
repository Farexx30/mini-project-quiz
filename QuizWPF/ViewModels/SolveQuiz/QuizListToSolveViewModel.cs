using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using QuizWPF.ViewModels.GenerateQuiz;
using QuizWPF.Views.SolveQuiz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.SolveQuiz
{
    public class QuizListToSolveViewModel : ViewModelBase
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
                OnPropertyChanged(nameof(NavigationService));
            }
        }

        private List<QuizDto> _allQuizzes = [];

        //Bindings:
        private ObservableCollection<QuizDto> _allFilteredQuizzes = [];
        public ObservableCollection<QuizDto> AllFilteredQuizzes
        {
            get => _allFilteredQuizzes;
            set
            {
                _allFilteredQuizzes = value;
                OnPropertyChanged(nameof(AllFilteredQuizzes));
            }
        }

        private QuizDto? _selectedQuiz;
        public QuizDto? SelectedQuiz
        {
            get => _selectedQuiz;
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }

        private uint _quizFilterIndex = 0;
        public uint QuizFilterIndex
        {
            get => _quizFilterIndex;
            set
            {
                _quizFilterIndex = value;
                OnPropertyChanged(nameof(QuizFilterIndex));
                FilterQuizzes();
            }
        }


        public RelayCommand NavigateToMainMenuCommand { get; set; } = null!;
        public RelayCommand NavigateToSelectedQuizCommand { get; set; } = null!;


        public QuizListToSolveViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService, IQuizRepositoryService quizRepositoryService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;
            _quizRepositoryService = quizRepositoryService;

            NavigateToMainMenuCommand = new RelayCommand(NavigateToMainMenu, o => true);
            NavigateToSelectedQuizCommand = new RelayCommand(NavigateToSelectedQuiz, CanSolve);

            Initialize();
        }

        private void Initialize()
        {
            _allQuizzes = _quizRepositoryService.GetAllQuizzes();
            AllFilteredQuizzes = new(_allQuizzes);
        }

        private void NavigateToMainMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();

        private void NavigateToSelectedQuiz(object obj)
        {
            _sharedQuizDataService.CurrentQuizDto = SelectedQuiz;

            NavigationService.NavigateTo<QuestionSolveViewModel>();
        }

        private void FilterQuizzes()
        {
            if (QuizFilterIndex == 0)
            {
                AllFilteredQuizzes = new(_allQuizzes);
            }
            else
            {
                AllFilteredQuizzes = new(_allQuizzes
                    .Where(q => (uint)q.Category == QuizFilterIndex - 1));
            }
        }


        //CanExecute:
        private bool CanSolve(object obj) => SelectedQuiz is not null;
    }
}