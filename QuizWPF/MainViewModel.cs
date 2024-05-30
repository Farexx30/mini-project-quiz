using QuizWPF.Commands;
using QuizWPF.Services;
using QuizWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuizWPF
{
    public class MainViewModel : ViewModelBase
    {
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

        public RelayCommand NavigateToMenuCommand { get; set; } = null!;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToMenuCommand = new RelayCommand(NavigateToMenu, o => true);

            NavigateToMenu(this);
        }

        private void NavigateToMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();
    }   
}
