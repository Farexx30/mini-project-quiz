using QuizWPF.Commands;
using QuizWPF.Models.Dtos;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class QuizQuestionsListViewModel : ViewModelBase
    {
        private readonly ISharedQuizDataService _sharedDataService;
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
            _sharedDataService = sharedQuizDataService;
            _quizRepositoryService = quizRepositoryService;           

            NavigateToQuizDetailsCommand = new RelayCommand(NavigateToQuizDetails, o => true);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion, o => true);
            NavigateToNewQuestionCreatorCommand = new RelayCommand(AddNewQuestion, o => true);
            NavigateToExistingQuestionModifierCommand = new RelayCommand(ModifyExistingQuestion, o => true);
            SaveQuizCommand = new RelayCommand(SaveQuiz, o => true);

            Initialize();
        }

        //Initializing:
        private void Initialize()
        {
            QuizQuestions = new(_sharedDataService.CurrentQuizDto.Questions);
        }


        public void SetMode(Mode mode) => _mode = mode;


        private void NavigateToQuizDetails(object obj)
        {
            NavigationService.NavigateTo<QuizDetailsViewModel>(_mode);

            QuestionDto.QuestionNumber = 1;
        }
        
        private void DeleteQuestion(object obj)
        {
            if (SelectedQuestion is not null) //Te zabezpieczenia to tak na szybko, a pewnie inaczej sie je zrobi i rozbuduje i tak.
            {
                QuizQuestions.First().Value = "HAHAHA";
                _sharedDataService.CurrentQuizDto.Questions.Remove(SelectedQuestion);
                QuizQuestions.Remove(SelectedQuestion);

                MessageBox.Show("Usuwam pytanie...");
            }           
        }

        private void AddNewQuestion(object obj)
        {
             //byc moze jakas logika jeszcze...
             MessageBox.Show("Dodaje pytanie...");

             NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);

             var newQuestionDto = new QuestionDto();
             QuizQuestions.Add(newQuestionDto);
             _sharedDataService.CurrentQuizDto.Questions.Add(newQuestionDto);
             _sharedDataService.CurrentQuestionDto = newQuestionDto;

             QuestionDto.QuestionNumber = 1;         
        }

        private void ModifyExistingQuestion(object obj)
        {
            //byc moze jakas logika
            if(SelectedQuestion is not null) //Te zabezpieczenia to tak na szybko, a pewnie inaczej sie je zrobi i rozbuduje i tak.
            {
                MessageBox.Show("Modyfikuje pytanie...");

                NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);

                _sharedDataService.CurrentQuestionDto = SelectedQuestion;

                QuestionDto.QuestionNumber = 1;
            }
           
        }

        private void SaveQuiz(object obj)
        {
            if (_mode == Mode.Add)
            {
                MessageBox.Show("Wygenerowano nowy quiz");
            }
            else
            {
                MessageBox.Show("Zaktualizowano quiz");
            }

            NavigationService.NavigateTo<QuizConfirmationViewModel>(_mode);

            QuestionDto.QuestionNumber = 1;
        }
    }


}
