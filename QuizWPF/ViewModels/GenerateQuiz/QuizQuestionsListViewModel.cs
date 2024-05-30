using QuizWPF.Commands;
using QuizWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class QuizQuestionsListViewModel : ViewModelBase
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

        public RelayCommand NavigateToQuizDetailsCommand { get; set; } = null!;
        public RelayCommand DeleteQuestionCommand { get; set; } = null!;
        public RelayCommand NavigateToNewQuestionCreatorCommand { get; set; } = null!;
        public RelayCommand NavigateToExistingQuestionModifierCommand { get; set; } = null!;
        public RelayCommand SaveQuizCommand { get; set; } = null!;

        public QuizQuestionsListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToQuizDetailsCommand = new RelayCommand(NavigateToQuizDetails, o => true);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion, o => true);
            NavigateToNewQuestionCreatorCommand = new RelayCommand(AddNewQuestion, o => true);
            NavigateToExistingQuestionModifierCommand = new RelayCommand(ModifyExistingQuestion, o => true);
            SaveQuizCommand = new RelayCommand(SaveQuiz, o => true);
        }

        public void SetMode(Mode mode) => _mode = mode;


        private void NavigateToQuizDetails(object obj) => NavigationService.NavigateTo<QuizDetailsViewModel>(_mode);
        private void DeleteQuestion(object obj)
        {
            MessageBox.Show("Usuwam pytanie...");
        }

        private void AddNewQuestion(object obj)
        {
            //byc moze jakas logika
            MessageBox.Show("Dodaje pytanie...");
            NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);
        }

        private void ModifyExistingQuestion(object obj)
        {
            //byc moze jakas logika
            MessageBox.Show("Modyfikuje pytanie...");
            NavigationService.NavigateTo<ModifyQuestionViewModel>(_mode);
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
        }
    }


}
