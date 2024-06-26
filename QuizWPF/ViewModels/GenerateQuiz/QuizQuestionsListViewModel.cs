﻿using QuizWPF.Commands;
using QuizWPF.Models;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class QuizQuestionsListViewModel : ViewModelBase
    {
        private readonly ISharedQuizDataService _sharedQuizDataService;
        private readonly IQuizRepositoryService _quizRepositoryService;

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


        //Bindings:
        private ObservableCollection<QuestionDto> _quizQuestions = [];
        public ObservableCollection<QuestionDto> QuizQuestions
        {
            get => _quizQuestions;
            set
            {
                _quizQuestions = value;
                OnPropertyChanged(nameof(QuizQuestions));
            }
        }

        private QuestionDto? _selectedQuestion = null;
        public QuestionDto? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        public RelayCommand NavigateToQuizDetailsCommand { get; set; } = null!;
        public RelayCommand DeleteQuestionCommand { get; set; } = null!;
        public RelayCommand NavigateToNewQuestionCreatorCommand { get; set; } = null!;
        public RelayCommand NavigateToExistingQuestionModifierCommand { get; set; } = null!;
        public RelayCommand SaveQuizCommand { get; set; } = null!;

        public QuizQuestionsListViewModel(INavigationService navigationService, ISharedQuizDataService sharedQuizDataService, IQuizRepositoryService quizRepositoryService)
        {
            _navigationService = navigationService;
            _sharedQuizDataService = sharedQuizDataService;
            _quizRepositoryService = quizRepositoryService;           

            NavigateToQuizDetailsCommand = new RelayCommand(NavigateToQuizDetails, _ => true);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion, CanDeleteOrEdit);
            NavigateToNewQuestionCreatorCommand = new RelayCommand(AddNewQuestion, _ => true);
            NavigateToExistingQuestionModifierCommand = new RelayCommand(ModifyExistingQuestion, CanDeleteOrEdit);
            SaveQuizCommand = new RelayCommand(SaveQuiz, CanSave);

            Initialize();
        }


        //Initializing:
        private void Initialize()
        {
            QuizQuestions = new(_sharedQuizDataService.CurrentQuizDto!.Questions);
        }


        public void SetMode(Mode mode) => _mode = mode;


        //I TU JESZCZE IMPLEMENTACJA DLA MODYFIKACJI QUIZÓW:
        private void NavigateToQuizDetails(object obj)
        {
            _sharedQuizDataService.CurrentQuestionDto = null;

            NavigationService.NavigateTo<QuizDetailsViewModel>(_mode);
        }
        
        private void DeleteQuestion(object obj)
        {
            _sharedQuizDataService.CurrentQuizDto!.Questions.Remove(SelectedQuestion!);
            QuizQuestions.Remove(SelectedQuestion!);
            _sharedQuizDataService.CurrentQuestionDto = null;        
        }

        private void AddNewQuestion(object obj)
        {
            var newQuestionDto = new QuestionDto()
            {
                QuizId = _sharedQuizDataService.CurrentQuizDto!.Id
            };
            QuizQuestions.Add(newQuestionDto);
            _sharedQuizDataService.CurrentQuizDto.Questions.Add(newQuestionDto);
            _sharedQuizDataService.CurrentQuestionDto = newQuestionDto;

            NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);       
        }

        private void ModifyExistingQuestion(object obj)
        {
            _sharedQuizDataService.CurrentQuestionDto = SelectedQuestion;

            NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);        
        }

        private void SaveQuiz(object obj)
        {
            if (_mode == Mode.Add)
            {
                _quizRepositoryService.AddNewQuiz(_sharedQuizDataService.CurrentQuizDto!);
            }
            else
            {
                _quizRepositoryService.UpdateExistingQuiz(_sharedQuizDataService.CurrentQuizDto!);
            }
           
            _sharedQuizDataService.CurrentQuizDto = null;
            _sharedQuizDataService.CurrentQuestionDto = null;

            NavigationService.NavigateTo<QuizConfirmationViewModel>(_mode);
        }

        //Validation:
        private bool CanSave(object obj) => QuizQuestions.Count >= 3;
        private bool CanDeleteOrEdit(object obj) => SelectedQuestion is not null;
    }


}
