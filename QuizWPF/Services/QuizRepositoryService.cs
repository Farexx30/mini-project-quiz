using QuizWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Services
{
    public interface IQuizRepositoryService
    {
        QuizDbContext DbContext { get; }
    }

    public class QuizRepositoryService(QuizDbContext dbContext) : IQuizRepositoryService
    {
        public QuizDbContext DbContext { get; } = dbContext;

        //Tutaj beda metody z operacjami na bazie danych takie jak pobierz/zapisz/zaktualizuj etc.
    }
}
