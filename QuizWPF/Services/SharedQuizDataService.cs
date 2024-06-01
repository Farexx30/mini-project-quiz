using QuizWPF.Models;
using QuizWPF.Models.Dtos;
using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Services
{
    public interface ISharedQuizDataService
    {
        QuizDto CurrentQuizDto { get; set; }
        QuestionDto? CurrentQuestionDto { get; set; }
        void ClearCurrentQuizData();
    }

    public class SharedQuizDataService : ISharedQuizDataService
    {
        public QuizDto CurrentQuizDto { get; set; } = new();
        public QuestionDto? CurrentQuestionDto { get; set; }

        public void ClearCurrentQuizData()
        {
            CurrentQuizDto.Name = string.Empty;
            CurrentQuizDto.Category = Category.Games;
            CurrentQuizDto.Questions.Clear();
            CurrentQuestionDto = null; //Teoretycznie niepotrzebne.
        }
    }
}
