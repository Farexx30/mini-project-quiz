using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizWPF.Models;
using QuizWPF.Models.Dtos;
using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizWPF.Services
{
    public interface IQuizRepositoryService
    {
        void AddNewQuiz(QuizDto newQuizDto);
        void UpdateExistingQuiz(QuizDto modifiedQuizDto);
        List<QuizDto> GetAllQuizzes();
        List<QuestionDto> GetQuizData(int quizId);
    }

    public class QuizRepositoryService(QuizDbContext dbContext, IMapper mapper) : IQuizRepositoryService
    {
        private readonly QuizDbContext _dbContext  = dbContext;
        private readonly IMapper _mapper  = mapper;


        public void AddNewQuiz(QuizDto newQuizDto)
        {
            var newQuiz = _mapper.Map<Quiz>(newQuizDto);

            try
            {
                _dbContext.Quizzes.Add(newQuiz);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił błąd z bazą danych");
            }
        }

        public void UpdateExistingQuiz(QuizDto modifiedQuizDto)
        {
            var modifiedQuiz = _mapper.Map<Quiz>(modifiedQuizDto);

            using(var updateTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _ = _dbContext.Quizzes
                    .Where(q => q.Id == modifiedQuiz.Id)
                    .ExecuteUpdate(p =>
                        p.SetProperty(q => q.Name, q => modifiedQuiz.Name)
                        .SetProperty(q => q.Category, q => modifiedQuiz.Category));

                    _ = _dbContext.Questions
                        .Where(q => q.QuizId == modifiedQuiz.Id)
                        .ExecuteDelete();

                    _dbContext.Questions.AddRange(modifiedQuiz.Questions);

                    _dbContext.SaveChanges();

                    updateTransaction.Commit();
                }    
                catch (Exception)
                {
                    updateTransaction.Rollback();
                    MessageBox.Show("Wystąpił błąd z bazą danych");
                }
            }           
        }

        public List<QuizDto> GetAllQuizzes()
        {
            try
            {
                var allQuizzes = _dbContext.Quizzes
                    .AsNoTracking()
                    .ToList();

                var allQuizzesDto = _mapper.Map<List<QuizDto>>(allQuizzes);
                return allQuizzesDto;
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił błąd z bazą danych");
                return [];
            }
        }

        public List<QuestionDto> GetQuizData(int quizId)
        {
            try
            {
                var allQuizQuestions = _dbContext.Questions
                    .AsNoTracking()
                    .Include(a => a.Answers)
                    .Where(q => q.QuizId == quizId)
                    .ToList();

                var allQuizQuestionsDtos = _mapper.Map<List<QuestionDto>>(allQuizQuestions);

                return allQuizQuestionsDtos;
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił błąd z bazą danych");
                return [];
            }
        }
    }
}
