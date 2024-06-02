using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace QuizWPF.ViewModels.SolveQuiz
{
    public class QuestionSolveViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private int _questionNumber;
        private uint _numberOfQuizQuestions;
        private uint _score;
        private QuestionDto? _currentQuestion;

        private DispatcherTimer _timer = null!;
        private TimeSpan _time;

        private ISharedQuizDataService _sharedQuizDataService;
        private ISharedSolvedQuizDataService _sharedSolvedQuizDataService;
        private IQuizRepositoryService _quizRepositoryService;
        
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
        private bool _isQuizStarted = false;
        public bool IsQuizStarted
        {
            get => _isQuizStarted;
            set
            {
                _isQuizStarted = value;
                OnPropertyChanged(nameof(IsQuizStarted));
            }
        }


        private string _currentQuestionNumber = null!;
        public string CurrentQuestionNumber
        {
            get => _currentQuestionNumber;
            set
            {
                _currentQuestionNumber = value;
                OnPropertyChanged(nameof(CurrentQuestionNumber));
            }
        }

        private string _questionValue = null!;
        public string QuestionValue
        {
            get => _questionValue;
            set
            {
                _questionValue = value;
                OnPropertyChanged(nameof(QuestionValue));
            }
        }

        private string _answerAValue = null!;
        public string AnswerAValue
        {
            get => _answerAValue;
            set
            {
                _answerAValue = value;
                OnPropertyChanged(nameof(AnswerAValue));
            }
        }

        private string _answerBValue = null!;
        public string AnswerBValue
        {
            get => _answerBValue;
            set
            {
                _answerBValue = value;
                OnPropertyChanged(nameof(AnswerBValue));
            }
        }

        private string _answerCValue = null!;
        public string AnswerCValue
        {
            get => _answerCValue;
            set
            {
                _answerCValue = value;
                OnPropertyChanged(nameof(AnswerCValue));
            }
        }

        private string _answerDValue = null!;
        public string AnswerDValue
        {
            get => _answerDValue;
            set
            {
                _answerDValue = value;
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


        public RelayCommand StartQuizCommand { get; set; } = null!;
        public RelayCommand EndQuizCommand { get; set; } = null!;
        public RelayCommand GoToNextQuestionCommand { get; set; } = null!;


        public QuestionSolveViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService, ISharedSolvedQuizDataService sharedSolvedQuizDataService, IQuizRepositoryService quizRepositoryService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;
            _sharedSolvedQuizDataService = sharedSolvedQuizDataService;
            _quizRepositoryService = quizRepositoryService;

            StartQuizCommand = new RelayCommand(StartQuiz, CanExecuteStartQuizCommand);
            EndQuizCommand = new RelayCommand(EndQuiz, CanExecuteEndQuizCommand);
            GoToNextQuestionCommand = new RelayCommand(GoToNextQuestion, CanExecuteGoToNextQuestionCommand);

            Initialize();
        }

        private void Initialize()
        {
            _sharedQuizDataService.CurrentQuizDto!.Questions = _quizRepositoryService.GetQuizData(_sharedQuizDataService.CurrentQuizDto.Id);
            _numberOfQuizQuestions = (uint)_sharedQuizDataService.CurrentQuizDto.Questions.Count;

            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += TimerTick;
        }


        //Command connected methods:
        private void StartQuiz(object obj)
        {
            GoToNextQuestion(this);
            StartTimer();

            IsQuizStarted = true;
        }

        private void EndQuiz(object obj)
        {
            StopTimer();
            if (IsAnsweredCorrectly()) _score++;

            _sharedSolvedQuizDataService.Score = _score;
            _sharedSolvedQuizDataService.MaxScore = _numberOfQuizQuestions;
            _sharedSolvedQuizDataService.TimeElapsed = TimeElapsed;

            _sharedQuizDataService.CurrentQuizDto = null;
            _sharedQuizDataService.CurrentQuestionDto = null;
            _currentQuestion = null; //I tak raczej niepotrzebne.

            NavigationService.NavigateTo<QuizResultsViewModel>();
        }

        private void GoToNextQuestion(object obj)
        {
            if (_questionNumber != 0 && _questionNumber < _numberOfQuizQuestions)
            {
                if (IsAnsweredCorrectly()) _score++;
            }
            if (_questionNumber < _numberOfQuizQuestions)
            {
                CurrentQuestionNumber = $"Pytanie {_questionNumber + 1} z {_numberOfQuizQuestions}";

                _sharedQuizDataService.CurrentQuestionDto = _sharedQuizDataService.CurrentQuizDto!.Questions[_questionNumber++];
                _currentQuestion = _sharedQuizDataService.CurrentQuestionDto;

                QuestionValue = _currentQuestion.Value;
                AnswerAValue = _currentQuestion.Answers[0].Value;
                AnswerBValue = _currentQuestion.Answers[1].Value;
                AnswerCValue = _currentQuestion.Answers[2].Value;
                AnswerDValue = _currentQuestion.Answers[3].Value;
                IsASelected = false;
                IsBSelected = false;
                IsCSelected = false;
                IsDSelected = false;
            }
            else
            {
                EndQuiz(this);
            }
        }

        private bool IsAnsweredCorrectly()
        {
            return (_currentQuestion!.Answers[0].IsCorrect && _isASelected || !_currentQuestion.Answers[0].IsCorrect && !_isASelected)
                && (_currentQuestion.Answers[1].IsCorrect && _isBSelected || !_currentQuestion.Answers[1].IsCorrect && !_isBSelected)
                && (_currentQuestion.Answers[2].IsCorrect && _isCSelected || !_currentQuestion.Answers[2].IsCorrect && !_isCSelected)
                && (_currentQuestion.Answers[3].IsCorrect && _isDSelected || !_currentQuestion.Answers[3].IsCorrect && !_isDSelected);
        }


        //Timer:
        private void TimerTick(object? sender, EventArgs e)
        {
            _time = _time.Add(TimeSpan.FromSeconds(1));
            OnPropertyChanged(nameof(TimeElapsed));
        }

        public string TimeElapsed
        {
            get => _time.ToString(@"mm\:ss");
        }

        private void StartTimer() => _timer.Start();
        private void StopTimer() => _timer.Stop();


        //CanExecute:
        private bool CanExecuteStartQuizCommand(object obj) => !IsQuizStarted;
        private bool CanExecuteEndQuizCommand(object obj) => IsQuizStarted;
        private bool CanExecuteGoToNextQuestionCommand(object obj) => IsQuizStarted && (_isASelected || _isBSelected || _isCSelected || _isDSelected);   
    }
}
