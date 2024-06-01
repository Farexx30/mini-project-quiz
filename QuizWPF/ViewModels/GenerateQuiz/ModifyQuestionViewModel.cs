﻿using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class ModifyQuestionViewModel : ViewModelBase
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


        public RelayCommand NavigateToQuizQuestionsListCommand { get; set; } = null!;

        public ModifyQuestionViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;

            NavigateToQuizQuestionsListCommand = new RelayCommand(NavigateToQuizQuestionsList, o => true);

            Initialize();
        }

        private void Initialize()
        {
            if (_mode == Mode.Add)
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
            else
            {
                    //??? Dla Mode.Modify (wiekszy problem).
            }
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

            NavigationService.NavigateTo<QuizQuestionsListViewModel>(_mode);
        }

    }
}
