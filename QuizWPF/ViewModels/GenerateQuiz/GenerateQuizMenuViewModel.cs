﻿using QuizWPF.Commands;
using QuizWPF.Services;
using QuizWPF.Views.GenerateQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizWPF.ViewModels.GenerateQuiz
{
    public class GenerateQuizMenuViewModel : ViewModelBase
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

        public RelayCommand NavigateToNewQuizGeneratorCommand { get; set; } = null!;
        public RelayCommand NavigateToQuizModifierCommand { get; set; } = null!;
        public RelayCommand NavigateToMainMenuCommand { get; set; } = null!;


        public GenerateQuizMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToNewQuizGeneratorCommand = new RelayCommand(NavigateToNewQuizGenerator, o => true);
            NavigateToQuizModifierCommand = new RelayCommand(NavigateToQuizModifier, o => true);
            NavigateToMainMenuCommand = new RelayCommand(NavigateToMainMenu, o => true);
        }

        private void NavigateToNewQuizGenerator(object obj) => NavigationService.NavigateTo<QuizDetailsViewModel>(Mode.Add);
        private void NavigateToQuizModifier(object obj) => NavigationService.NavigateTo<ChooseQuizToModifyViewModel>();
        private void NavigateToMainMenu(object obj) => NavigationService.NavigateTo<MenuViewModel>();
    }
}