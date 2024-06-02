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
        QuizDto? CurrentQuizDto { get; set; }
        QuestionDto? CurrentQuestionDto { get; set; }
    }

    public interface ISharedSolvedQuizDataService
    {
        uint Score { get; set; }
        uint MaxScore { get; set; }
        string TimeElapsed { get; set; }
    }


    public class SharedQuizDataService : ISharedQuizDataService, ISharedSolvedQuizDataService
    {
        public QuizDto? CurrentQuizDto { get; set; }
        public QuestionDto? CurrentQuestionDto { get; set; }


        //For solved quiz statistics:
        public uint Score { get; set; }
        public uint MaxScore { get; set; }
        public string TimeElapsed { get; set; } = null!;
    }
}
