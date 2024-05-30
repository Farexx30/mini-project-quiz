using QuizWPF.ViewModels;
using QuizWPF.ViewModels.GenerateQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Services
{
    public interface INavigationService
    {
        ViewModelBase? CurrentViewModel { get; }
        void NavigateTo<T>(Mode mode = Mode.Add) where T : ViewModelBase;
    }

    public class NavigationService(Func<Type, ViewModelBase> viewModelFactory) : ViewModelBase, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory = viewModelFactory;
        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel; 
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }        
        }

        public void NavigateTo<T>(Mode mode = Mode.Add) where T : ViewModelBase
        {
            ViewModelBase newViewModel = _viewModelFactory.Invoke(typeof(T));

            if (newViewModel is QuizDetailsViewModel quizDetailsViewModel)
            {
                quizDetailsViewModel.SetMode(mode);
            }
            else if (newViewModel is QuizQuestionsListViewModel quizQuestionsListViewModel)
            {
                quizQuestionsListViewModel.SetMode(mode);
            }
            else if(newViewModel is ModifyQuestionViewModel modifyQuestionViewModel)
            {
                modifyQuestionViewModel.SetMode(mode);
            }

            CurrentViewModel = newViewModel;
        }
    }
}
