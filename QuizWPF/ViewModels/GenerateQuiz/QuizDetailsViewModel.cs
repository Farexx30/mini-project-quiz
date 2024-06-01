using QuizWPF.Commands;
using QuizWPF.Models;
using QuizWPF.Models.Entities;
using QuizWPF.Services;
using QuizWPF.ViewModels.SolveQuiz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class QuizDetailsViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ISharedQuizDataService _sharedQuizDataService;
        private Mode _mode;

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

        //Error info:
        Dictionary<string, List<string>> _Errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _Errors.Count>0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if(_Errors.TryGetValue(propertyName!, out List<string>? value))
            {
                return value;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        //Validation:
        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if(results.Count != 0) 
            {
                _Errors.Add(propertyName, results.Select(r => r.ErrorMessage!).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                _Errors.Remove(propertyName);
            }
        }
        private bool CanSubmit(object obj) => Validator.TryValidateObject(this, new ValidationContext(this), null);

        //Bindings:
        private string _quizTitle = string.Empty;
        [Required(ErrorMessage ="Podaj tytuł")]
        public string QuizTitle
        {
            get => _quizTitle;
            set
            {
                _quizTitle = value;
                Validate(nameof(QuizTitle), value);
                OnPropertyChanged(nameof(QuizTitle));
            }
        }

        private uint _quizCategory;
        public uint QuizCategory
        {
            get => _quizCategory;
            set
            {
                _quizCategory = value;
                OnPropertyChanged(nameof(QuizCategory));
            }
        }

        public RelayCommand NavigateToPreviousCommand { get; set; } = null!;
        public RelayCommand NextButtonClickCommand { get; set; } = null!;

        
        public QuizDetailsViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;

            NavigateToPreviousCommand = new RelayCommand(NavigateToPrevious, o => true);
            NextButtonClickCommand = new RelayCommand(NextButtonClick, CanSubmit);

            Initialize();
        }

        //Initializing:
        private void Initialize()
        {
            QuizTitle = _sharedQuizDataService.CurrentQuizDto.Name;
            QuizCategory = (uint)_sharedQuizDataService.CurrentQuizDto.Category;
        }


        //Setting mode to ensure, that ViewModel will behave correctly when adding and modifying quiz
        public void SetMode(Mode mode) => _mode = mode;


        //Navigation:
        private void NavigateToPrevious(object obj)
        {
            if (_mode == Mode.Add) NavigationService.NavigateTo<GenerateQuizMenuViewModel>();
            else NavigationService.NavigateTo<ChooseQuizToModifyViewModel>();

            _sharedQuizDataService.ClearCurrentQuizData();
        }
        private void NavigateToQuestionList() => NavigationService.NavigateTo<QuizQuestionsListViewModel>(_mode);


        //Buttons logic:
        private void NextButtonClick(object obj)
        {
            _sharedQuizDataService.CurrentQuizDto.Name = QuizTitle;
            _sharedQuizDataService.CurrentQuizDto.Category = (Category)QuizCategory;           

            NavigateToQuestionList();
        }

    }
}
