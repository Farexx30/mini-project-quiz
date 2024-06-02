using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class ModifyQuestionViewModel : ViewModelBase, INotifyDataErrorInfo
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
                OnPropertyChanged();
            }
        }


        private readonly Dictionary<string, List<string>> _errors = [];

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _errors.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.TryGetValue(propertyName!, out List<string>? value))
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

            if (results.Count != 0)
            {
                _errors.Add(propertyName, results.Select(r => r.ErrorMessage!).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                _errors.Remove(propertyName);
            }
        }


        private string _questionValue = null!;
        [Required(ErrorMessage = "Podaj treść pytania")]
        public string QuestionValue
        {
            get => _questionValue;
            set
            {
                _questionValue = value;
                Validate(nameof(QuestionValue), value);
                OnPropertyChanged(nameof(QuestionValue));
            }
        }

        private string _answerAValue = null!;
        [Required(ErrorMessage = "Podaj treść odpowiedzi")]
        public string AnswerAValue
        {
            get => _answerAValue;
            set
            {
                _answerAValue = value;
                Validate(nameof(AnswerAValue), value);
                OnPropertyChanged(nameof(AnswerAValue));
            }
        }

        private string _answerBValue = null!;
        [Required(ErrorMessage = "Podaj treść odpowiedzi")]
        public string AnswerBValue
        {
            get => _answerBValue;
            set
            {
                _answerBValue = value;
                Validate(nameof(AnswerBValue), value);
                OnPropertyChanged(nameof(AnswerBValue));
            }
        }

        private string _answerCValue = null!;
        [Required(ErrorMessage = "Podaj treść odpowiedzi")]
        public string AnswerCValue
        {
            get => _answerCValue;
            set
            {
                _answerCValue = value;
                Validate(nameof(AnswerCValue), value);
                OnPropertyChanged(nameof(AnswerCValue));
            }
        }

        private string _answerDValue = null!;
        [Required(ErrorMessage = "Podaj treść odpowiedzi")]
        public string AnswerDValue
        {
            get => _answerDValue;
            set
            {
                _answerDValue = value;
                Validate(nameof(AnswerDValue), value);
                OnPropertyChanged(nameof(AnswerDValue));
            }
        }

        private bool _isASelected;
        public bool IsASelected
        {
            get => _isASelected;
            set
            {
                _isASelected = value;
                OnPropertyChanged(nameof(IsASelected));
            }
        }

        private bool _isBSelected;
        public bool IsBSelected
        {
            get => _isBSelected;
            set
            {
                _isBSelected = value;
                OnPropertyChanged(nameof(IsBSelected));
            }
        }

        private bool _isCSelected;
        public bool IsCSelected
        {
            get => _isCSelected;
            set
            {
                _isCSelected = value;
                OnPropertyChanged(nameof(IsCSelected));
            }
        }

        private bool _isDSelected;
        public bool IsDSelected
        {
            get => _isDSelected;
            set
            {
                _isDSelected = value;
                OnPropertyChanged(nameof(IsDSelected));
            }
        }


        public RelayCommand NavigateToQuizQuestionsListCommand { get; set; } = null!;

        public ModifyQuestionViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;

            NavigateToQuizQuestionsListCommand = new RelayCommand(NavigateToQuizQuestionsList, CanSubmit);

            Initialize();
        }

        private bool CanSubmit(object obj) => Validator.TryValidateObject(this, new ValidationContext(this), null) && (IsASelected || IsBSelected || IsCSelected || IsDSelected);
        
        private void Initialize()
        {
            QuestionValue = _sharedQuizDataService.CurrentQuestionDto!.Value;
            AnswerAValue = _sharedQuizDataService.CurrentQuestionDto.Answers[0].Value;
            AnswerBValue = _sharedQuizDataService.CurrentQuestionDto.Answers[1].Value;
            AnswerCValue = _sharedQuizDataService.CurrentQuestionDto.Answers[2].Value;
            AnswerDValue = _sharedQuizDataService.CurrentQuestionDto.Answers[3].Value;
            IsASelected = _sharedQuizDataService.CurrentQuestionDto.Answers[0].IsCorrect;
            IsBSelected = _sharedQuizDataService.CurrentQuestionDto.Answers[1].IsCorrect;
            IsCSelected = _sharedQuizDataService.CurrentQuestionDto.Answers[2].IsCorrect;
            IsDSelected = _sharedQuizDataService.CurrentQuestionDto.Answers[3].IsCorrect;
        }

        public void SetMode(Mode mode) => _mode = mode;

        private void NavigateToQuizQuestionsList(object obj)
        {
            _sharedQuizDataService.CurrentQuestionDto!.Value = QuestionValue;
            _sharedQuizDataService.CurrentQuestionDto.Answers[0].Value = AnswerAValue;
            _sharedQuizDataService.CurrentQuestionDto.Answers[1].Value = AnswerBValue;
            _sharedQuizDataService.CurrentQuestionDto.Answers[2].Value = AnswerCValue;
            _sharedQuizDataService.CurrentQuestionDto.Answers[3].Value = AnswerDValue;
            _sharedQuizDataService.CurrentQuestionDto.Answers[0].IsCorrect = IsASelected;
            _sharedQuizDataService.CurrentQuestionDto.Answers[1].IsCorrect = IsBSelected;
            _sharedQuizDataService.CurrentQuestionDto.Answers[2].IsCorrect = IsCSelected;
            _sharedQuizDataService.CurrentQuestionDto.Answers[3].IsCorrect = IsDSelected;

            _sharedQuizDataService.CurrentQuestionDto = null;

            NavigationService.NavigateTo<QuizQuestionsListViewModel>(_mode);
        }
    }
}
